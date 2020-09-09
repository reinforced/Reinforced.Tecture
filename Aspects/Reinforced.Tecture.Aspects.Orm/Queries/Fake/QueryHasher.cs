using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Reinforced.Tecture.Query;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Fake
{
    class QueryHasher : ExpressionVisitor, IDisposable
    {
        private readonly Hashbox _box = new Hashbox();

        /// <summary>Initializes a new instance of <see cref="T:System.Linq.Expressions.ExpressionVisitor"></see>.</summary>
        public QueryHasher()
        {

        }

        /// <summary>Dispatches the expression to one of the more specialized visit methods in this class.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        public override Expression Visit(Expression node)
        {
            if (node == null)
            {
                _box.PutNull();
            }
            else
            {
                _box.Put((int)node.NodeType);
            }
            return base.Visit(node);
        }


        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.CatchBlock"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            _box.Put(node.Test.GUID.ToByteArray());
            return base.VisitCatchBlock(node);
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.ConstantExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = node.Value;
            if (value is IQueryable q)
            {
                _box.Put(q.ElementType.FullName);
                if (q.Expression != node)
                {
                    Visit(q.Expression);
                }
                return node;
            }

            var type = node.Type;
            if (!type.IsAnonymousType()) _box.Put(type.FullName);
            else _box.Put("anonymous");

            if (value != null)
            {
                _box.Put(value);
            }
            else
            {
                _box.PutNull();
            }

            return node;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.GotoExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitGoto(GotoExpression node)
        {
            _box.Put((int)node.Kind);
            return base.VisitGoto(node);
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.LabelTarget"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            _box.Put(node.Name);
            return base.VisitLabelTarget(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.Expression`1"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <typeparam name="T">The type of the delegate.</typeparam>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            _box.Put(node.Name);
            return base.VisitLambda(node);
        }

        private void WriteMember(MemberInfo mi)
        {
            if (mi == null)
            {
                _box.Put(0);
                return;
            }
            _box.Put((byte)mi.MemberType);
            _box.Put(mi.Name);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            WriteMember(node.Member);
            var r = base.VisitMember(node);
            return r;
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberAssignment"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            WriteMember(node.Member);
            return base.VisitMemberAssignment(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberBinding"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            WriteMember(node.Member);
            _box.Put((byte)node.BindingType);
            return base.VisitMemberBinding(node);
        }


        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberListBinding"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            WriteMember(node.Member);
            _box.Put((byte)node.BindingType);
            return base.VisitMemberListBinding(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MemberMemberBinding"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            WriteMember(node.Member);
            _box.Put((byte)node.BindingType);
            return base.VisitMemberMemberBinding(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            WriteMember(node.Method);
            return base.VisitMethodCall(node);
        }

        /// <summary>Visits the <see cref="T:System.Linq.Expressions.ParameterExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            //todo
            return base.VisitParameter(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.SwitchExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            WriteMember(node.Comparison);
            return base.VisitSwitch(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.TypeBinaryExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            _box.Put(node.TypeOperand.GUID.ToByteArray());
            return base.VisitTypeBinary(node);
        }

        /// <summary>Visits the children of the <see cref="T:System.Linq.Expressions.UnaryExpression"></see>.</summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            WriteMember(node.Method);
            return base.VisitUnary(node);
        }


        public string GenerateHash()
        {
            return _box.Compute();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _box.Dispose();
        }
    }
}
