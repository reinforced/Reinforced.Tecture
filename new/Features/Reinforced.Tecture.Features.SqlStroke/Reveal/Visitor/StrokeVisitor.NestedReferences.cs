using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reinforced.Tecture.Features.SqlStroke.Reveal.Visitor
{
    partial class StrokeVisitor
    {
        private readonly List<NestedTableReference> _nestedTables = new List<NestedTableReference>();

        private NestedTableReference ObtainNestedTableReference(MemberExpression mex, bool derived)
        {
            // first, we make alias for nested table
            var alias = mex.MakeNestedTableAlias(derived);

            // obtain entity type that we address (x.User.Order -> Order in this case)
            var t = mex.Type;

            // if we want to take property from parent entity
            if (derived && t.BaseType != null) t = t.BaseType;

            //save used types
            _usedTypes.AddIfNotExists(t);

            // check whether we already have referenced this table
            var result = _nestedTables.FirstOrDefault(d => d.Alias == alias && d.EntityType == t);

            // if no, then it is time to
            if (result == null)
            {
                // first we need to understand - do we have "parent" of this table

                /*
                 * Let's say we have mex = x.User.Order
                 * then we must traverse further and also add reference
                 * to User along with Order
                 *
                 * if we have x.Order(.Id) - then join to Order will be enough
                 */

                // first we take parent ex (x.User in x.User.Order)
                var parentEx = mex.Expression;

                TableReference parent;

                // first we filter off the case when it is Parameter expression (x.Order and we've already extracted Order)
                if (parentEx.NodeType == ExpressionType.Parameter && parentEx is ParameterExpression pex)
                    parent = _tables[pex.Name];

                // if it is member expression (x.Order.User and we've already extracted User)
                else if (parentEx.NodeType == ExpressionType.MemberAccess && parentEx is MemberExpression mx)
                    parent = ObtainNestedTableReference(mx, mx.Expression.Type != mx.Member.DeclaringType);

                // if no then.. well
                else throw new Exception("Invalid entity table reference: " + parentEx);

                // then we construct the result
                result = new NestedTableReference(t)
                {
                    Alias = alias,
                    JoinColumn = mex.Member as PropertyInfo,
                    Parent = parent
                };

                // and link children and parents
                parent.Children.Add(result);

                // and we collect nested table
                _nestedTables.Add(result);
            }

            return result;
        }
    }
}
