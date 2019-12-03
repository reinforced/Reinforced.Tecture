using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Storage.Adapters;

namespace Reinforced.Storage.Strokes.Visitor
{
    static class TypeExtensions
    {
        public static bool IsEnumerable(this Type t)
        {
            if (t.IsArray) return true;
            if (typeof(IEnumerable).IsAssignableFrom(t)) return true;
            if (t.IsGenericType)
            {
                var tg = t.GetGenericTypeDefinition();
                if (typeof(IEnumerable<>).IsAssignableFrom(tg)) return true;
            }
            return false;
        }
    }
    class StrokeVisitor : ExpressionVisitor
    {
        private readonly Stack<SqlQueryExpression> _resultsStack = new Stack<SqlQueryExpression>();
        private readonly IMapper _adapter;
        private readonly string _format;
        private readonly Dictionary<string, TableParameterReference> _tables;

        public int ArgIdx { get; set; }

        private HashSet<Type> _usedTypes = new HashSet<Type>();

        public IEnumerable<Type> UsedTypes
        {
            get { return _usedTypes; }
        }

        public StrokeVisitor(IMapper adapter, string format, Dictionary<string, TableParameterReference> tables)
        {
            _adapter = adapter;
            _format = format;
            _tables = tables;
            foreach (var tpr in tables)
            {
                _usedTypes.AddIfNotExists(tpr.Value.EntityType);
            }
        }

        private void Return(SqlQueryExpression expr)
        {
            _resultsStack.Push(expr);
        }

        public SqlQueryExpression Retrieve()
        {
            return _resultsStack.Pop();
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.ConditionalExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Visit(node.Test);
            var test = Retrieve();
            Visit(node.IfTrue);
            var ifTrue = Retrieve();
            Visit(node.IfFalse);
            var ifFalse = Retrieve();
            Return(new SqlTernaryExpression() { Condition = test, IfTrue = ifTrue, IfFalse = ifFalse });

            return node;
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            var result = new SqlTableReference() { Table = _tables[node.Name] };

            if (_format.NeedsAlias(ArgIdx))
            {
                result.IsDeclaration = true;
                _tables[node.Name].IsDeclared = true;
            }

            Return(result);

            return node;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitMember(MemberExpression node)
        {
            Return(VisitColumn(node));
            return node;
        }

        private readonly List<TableParameterReference> _nestedTables = new List<TableParameterReference>();

        private TableParameterReference ObtainNestedTableReference(ParameterExpression mex, Type baseType, Type derivedType)
        {
            var alias = mex.GetNestedTableAlias(baseType);
            var t = baseType;
            _usedTypes.AddIfNotExists(t);
            var tableName = _adapter.GetTableName(t);
            var result = _nestedTables.FirstOrDefault(d => d.Alias == alias && d.TableName == tableName);
            if (result == null)
            {

                result = new TableParameterReference(t)
                {
                    Alias = alias,
                    TableName = tableName, 
                    IsDerivedJoin =  true
                };
                result.JoinColumn = null;
                result.Parent = _tables[mex.Name];
                _tables[mex.Name].Children.Add(result);
                
                _nestedTables.Add(result);
            }

            return result;
        }

        private TableParameterReference ObtainNestedTableReference(MemberExpression mex, bool derived)
        {
            var alias = mex.GetNestedTableAlias(derived);
            var t = mex.Type;
            if (derived && t.BaseType != null) t = t.BaseType;
            _usedTypes.AddIfNotExists(t);
            var tableName = _adapter.GetTableName(t);
            var result = _nestedTables.FirstOrDefault(d => d.Alias == alias && d.TableName == tableName);
            if (result == null)
            {

                result = new TableParameterReference(t)
                {
                    Alias = alias,
                    TableName = tableName
                };
                var parentEx = mex.Expression;
                TableParameterReference parent;
                if (parentEx.NodeType == ExpressionType.Parameter)
                {
                    parent = _tables[((ParameterExpression)parentEx).Name];
                }
                else if (parentEx.NodeType == ExpressionType.MemberAccess)
                {
                    var mx = (MemberExpression)parentEx;

                    parent = ObtainNestedTableReference((MemberExpression)parentEx, mx.Expression.Type != mx.Member.DeclaringType);
                }
                else throw new Exception("Invalid entity table reference: " + parentEx);

                result.JoinColumn = mex.Member as PropertyInfo;
                result.Parent = parent;

                parent.Children.Add(result);
                _nestedTables.Add(result);
            }

            return result;
        }
        

        private SqlQueryExpression VisitColumn(MemberExpression node)
        {
            if (!node.IsScopedParameterAccess())
            {
                return new SqlObjectParameter() { Parameter = ObtainResult<object>(node) };
            }

            var root = node.GetRootMember();
            if (_adapter.IsEntityType(node.Type)) //nested table reference
            {
                var nestedResult = new SqlTableReference()
                {
                    Table = ObtainNestedTableReference(node, false)
                };
                if (_format.NeedsAlias(ArgIdx))
                {
                    nestedResult.IsDeclaration = true;
                    nestedResult.Table.IsDeclared = true;
                }

                return nestedResult;
            }

            TableParameterReference tableRef = null;
            string colId = null;
            var nex = node.Expression.Unconvert();
            if (nex.NodeType != ExpressionType.Parameter)
            {
                if (nex.NodeType == ExpressionType.MemberAccess) //nested table column
                {
                    var mex = (MemberExpression)nex;
                    var derived = (node.Member.DeclaringType != mex.Type) && (_adapter.GetTableName(node.Member.DeclaringType) != null);
                    tableRef = ObtainNestedTableReference(mex, derived);
                    colId = _adapter.GetColumnName(derived ? node.Member.DeclaringType : mex.Type, node.Member.Name);
                }
                else throw new Exception(string.Format("Please refer only top-level properties of {0}", root.Type));
            }


            if (tableRef == null && colId == null)
            {
                var parRef = root as ParameterExpression;
                if (parRef == null) throw new Exception("Unknown column reference: " + node.ToString());
                tableRef = _tables[parRef.Name];
                var derived = (node.Member.DeclaringType != root.Type) && (_adapter.GetTableName(node.Member.DeclaringType) != null);
                colId = _adapter.GetColumnName(derived ? node.Member.DeclaringType : root.Type, node.Member.Name);
            }

            var result = new SqlColumnReference()
            {
                ColumnName = colId,
                Table = tableRef,
                IsAlias = _format.IsColumnAlias(ArgIdx)
            };


            return result;
        }

        private SqlBinaryExpression WrapAloneBoolean(SqlColumnReference boolColumn, bool negate)
        {
            return new SqlBinaryExpression()
            {
                Left = boolColumn,
                Right = new SqlQueryLiteralExpression() { Literal = "1" },
                Symbol = negate ? "<>" : "="
            };
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var unconverted = node.Unconvert();
            if (unconverted != node) return base.Visit(unconverted);
            SqlQueryExpression operand = null;

            var op = GetNodeSymbol(node.NodeType);
            Visit(node.Operand);
            operand = Retrieve();
            if (node.NodeType == ExpressionType.Not && node.Type == typeof(bool) && operand is SqlColumnReference)
            {
                var cr = operand as SqlColumnReference;
                var result = WrapAloneBoolean(cr, true);
                Return(result);
                return node;
            }
            Return(new SqlUnaryExpression() { Operand = operand, Symbol = op });
            return node;
        }

        private bool _isParseringSet = false;

        private bool IsSpecialSetSyntax(BinaryExpression node)
        {
            if (node.NodeType != ExpressionType.Or) return false;
            if (node.Left.NodeType != ExpressionType.Or && node.Left.NodeType != ExpressionType.Equal) return false;
            if (node.Right.NodeType != ExpressionType.Or && node.Right.NodeType != ExpressionType.Equal) return false;
            return true;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.BinaryExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (_format.IsSet(ArgIdx) && IsSpecialSetSyntax(node) && !_isParseringSet)
            {
                _isParseringSet = true;
                VisitSet(node);
                _isParseringSet = false;
                return node;
            }
            if (_format.IsClause(ArgIdx))
            {
                if ((IsNullConstant(node.Left) || IsNullConstant(node.Right)))
                {
                    if (IsNullConstant(node.Left)) Visit(node.Right);
                    else if (IsNullConstant(node.Right)) Visit(node.Left);
                    string oper = node.NodeType == ExpressionType.NotEqual ? "IS NOT" : "IS";
                    var lft = Retrieve();
                    Return(new SqlBinaryExpression()
                    {
                        Left = lft,
                        Right = new SqlQueryLiteralExpression() { Literal = "NULL" },
                        Symbol = oper
                    });
                    return node;
                }
            }
            var op = GetNodeSymbol(node.NodeType);
            Visit(node.Left);
            var left = Retrieve();
            Visit(node.Right);
            var right = Retrieve();
            if (node.NodeType != ExpressionType.Equal)
            {
                if ((node.Left.Type == typeof(bool) || node.Left.Type == typeof(bool?)) && left is SqlColumnReference)
                {
                    var col = left as SqlColumnReference;
                    left = WrapAloneBoolean(col, false);
                }

                if ((node.Right.Type == typeof(bool) || node.Right.Type == typeof(bool?)) && right is SqlColumnReference)
                {
                    var col = right as SqlColumnReference;
                    right = WrapAloneBoolean(col, false);
                }
            }
            Return(new SqlBinaryExpression() { Left = left, Right = right, Symbol = op });
            return node;
        }

        private void VisitSet(BinaryExpression node)
        {
            var result = new SqlSetExpression();
            ProceedPart(node.Left, result);
            ProceedPart(node.Right, result);
            Return(result);
        }

        private void ProceedPart(Expression ex, SqlSetExpression result)
        {
            if (ex.NodeType == ExpressionType.Or)
            {
                VisitSet((BinaryExpression)ex);
                var d = (SqlSetExpression)Retrieve();
                foreach (var sx in d.Assignments)
                {
                    result.Assignments.Add(sx);
                }
                return;
            }

            if (ex.NodeType == ExpressionType.Equal)
            {
                var setEx = (BinaryExpression)ex;
                var left = setEx.Left.Unconvert();
                var right = setEx.Right.Unconvert();
                if (left.NodeType == ExpressionType.MemberAccess)
                {
                    var mex = (MemberExpression)left;
                    var col = VisitColumn(mex) as SqlColumnReference;
                    if (col != null)
                    {
                        Visit(right);
                        var exp = Retrieve();
                        var assign = new SqlSetAssignmentExpression()
                        {
                            Column = col,
                            Expression = exp
                        };
                        result.Assignments.Add(assign);
                        return;
                    }
                }
            }
            throw new Exception("Invalid SET expression around " + ex.ToString());
        }

        private bool IsNullConstant(Expression ex)
        {
            if (ex.NodeType != ExpressionType.Constant) return false;
            return ((ConstantExpression)ex).Value == null;
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.ConstantExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = node.Value;
            var type = node.Type;

            if (value == null)
            {
                Return(new SqlQueryLiteralExpression { Literal = "NULL" });
                return node;
            }
            if (type == typeof(bool))
            {
                var b = (bool)value;
                Return(new SqlQueryLiteralExpression { Literal = b ? "1" : "0" });
                return node;
            }
            Return(new SqlObjectParameter() { Parameter = value });
            return node;
        }


        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression" />.</summary>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        /// <param name="node">The expression to visit.</param>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.GetCustomAttribute(typeof(StrokeTextAttribute), true) != null)
            {
                var result = ObtainResult<string>(node);
                Return(new SqlQueryLiteralExpression() { Literal = result ?? string.Empty });
                return node;
            }

            if (node.Method.Name == "Contains" && node.Arguments.Count == 2 && node.Arguments[0].Type.IsEnumerable())
            {
                var result = ObtainResult<IEnumerable>(node.Arguments[0]);
                Visit(node.Arguments[1]);
                var expr = Retrieve();
                Return(new SqlInExpression() { Expression = expr, Range = result == null ? new object[0] : result.Cast<object>().ToArray() });
                return node;
            }

            if (node.Method == _joinMethod)
            {
                var result = new SqlAutojoinExpression()
                {
                    Entities = new List<TableParameterReference>()
                };
                result.Join = (Join)(node.Arguments[0] as ConstantExpression).Value;
                var na = node.Arguments[1] as NewArrayExpression;
                for (int i = 0; i < na.Expressions.Count; i++)
                {
                    var pex = na.Expressions[i].Unconvert() as ParameterExpression;
                    TableParameterReference tref = null;
                    if (pex != null)
                    {
                        tref = _tables[pex.Name];
                    }
                    else throw new Exception("Invalid nested aggregate: " + na.Expressions[i]);

                    result.Entities.Add(tref);
                }
                Return(result);
                return node;
            }

            if (node.Method == _joinOverrideMethod)
            {
                var join = (Join)(node.Arguments[0] as ConstantExpression).Value;
                var mex = node.Arguments[1].Unconvert() as MemberExpression;
                if (mex == null) throw new Exception("Join overrides work only for nested aggregates");
                var tref = ObtainNestedTableReference(mex, false);
                tref.JoinOveride = join;
                Return(new SqlEmptyExpression());
                return node;
            }

            if (node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == _everyMethod)
            {

            }

            if (node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == _relationMethod)
            {

            }

            if (node.Method.ReturnType == typeof(void))
                throw new Exception("Cannot use void methods in queries");
            else
            {
                var result = ObtainResult<object>(node);
                Return(new SqlObjectParameter() { Parameter = result });
                return node;
            }
        }

        private T ObtainResult<T>(Expression ex)
        {
            var lambda = Expression.Lambda(ex);
            var compiled = lambda.Compile();
            var result = (T)compiled.DynamicInvoke();
            return result;
        }

        private string GetNodeSymbol(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.And: return "&";
                case ExpressionType.AndAlso: return "AND";
                case ExpressionType.Or: return "|";
                case ExpressionType.OrElse: return "OR";
                case ExpressionType.Not: return "NOT";
                case ExpressionType.Equal: return "=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Negate: return "-";
                case ExpressionType.UnaryPlus: return "+";
                case ExpressionType.Assign: return "=";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.Coalesce: return "??";
                case ExpressionType.Modulo: return "%";
                default:
                    throw new Exception("Invalid expression type");
            }
        }

        private static readonly MethodInfo _joinMethod;
        private static readonly MethodInfo _joinOverrideMethod;
        private static readonly MethodInfo _everyMethod;
        private static readonly MethodInfo _relationMethod;
        private static readonly MethodInfo _fromMethod;
        static StrokeVisitor()
        {
            _joinMethod = typeof(StrokeJoins).GetMethod("Nest");
            _joinOverrideMethod = typeof(StrokeJoins).GetMethod("Overjoin");

            _everyMethod = typeof(StrokeRelations).GetMethod("Every");
            _relationMethod = typeof(StrokeRelations).GetMethod("Relation");
            _fromMethod = typeof(StrokeRelations).GetMethod("From");
        }
    }
}
