using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Aspects.Orm.Queries
{
    public static class IncludeExtensions
    {
        #region Include

        internal static readonly MethodInfo IncludeMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Include))
                .Single(
                    mi =>
                        mi.GetGenericArguments().Count() == 2
                        && mi.GetParameters().Any(
                            pi => pi.Name == "navigationPropertyPath" && pi.ParameterType != typeof(string)));

        /// <summary>
        ///     Specifies related entities to include in the query results. The navigation property to be included is specified starting with the
        ///     type of entity being queried (<typeparamref name="TEntity" />). If you wish to include additional types based on the navigation
        ///     properties of the type being included, then chain a call to
        ///     <see
        ///         cref="ThenInclude{TEntity, TPreviousProperty, TProperty}(IIncludableQueryable{TEntity, IEnumerable{TPreviousProperty}}, Expression{Func{TPreviousProperty, TProperty}})" />
        ///     after this call.
        /// </summary>
        /// <example>
        ///     <para>
        ///         The following query shows including a single level of related entities:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts)</code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch:
        ///     </para>
        ///     <code>
        /// context.Blogs
        ///    .Include(blog => blog.Posts).ThenInclude(post => post.Tags)
        ///     </code>
        ///     <para>
        ///         The following query shows including multiple levels and branches of related data:
        ///     </para>
        ///     <code>
        /// context.Blogs
        ///    .Include(blog => blog.Posts).ThenInclude(post => post.Tags).ThenInclude(tag => tag.TagInfo)
        ///    .Include(blog => blog.Contributors)
        ///     </code>
        ///     <para>
        ///         The following query shows including a single level of related entities on a derived type using casting:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => ((SpecialBlog)blog).SpecialPosts)</code>
        ///     <para>
        ///         The following query shows including a single level of related entities on a derived type using 'as' operator:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => (blog as SpecialBlog).SpecialPosts)</code>
        /// </example>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <typeparam name="TProperty"> The type of the related entity to be included. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <param name="navigationPropertyPath">
        ///     A lambda expression representing the navigation property to be included (<c>t => t.Property1</c>).
        /// </param>
        /// <returns>
        ///     A new query with the related data included.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="navigationPropertyPath" /> is <see langword="null" />.
        /// </exception>
        public static IIncludableQueryable<TEntity, TProperty> Include<TEntity, TProperty>(
            this IQueryable<TEntity> source,
            Expression<Func<TEntity, TProperty>> navigationPropertyPath)
            where TEntity : class
        {

            Check.NotNull(navigationPropertyPath, nameof(navigationPropertyPath));

            return new IncludableQueryable<TEntity, TProperty>(
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: IncludeMethodInfo.MakeGenericMethod(typeof(TEntity), typeof(TProperty)),
                            arguments: new[] { source.Expression, Expression.Quote(navigationPropertyPath) }))
                    : source);
        }

        internal static readonly MethodInfo ThenIncludeAfterEnumerableMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude))
                .Where(mi => mi.GetGenericArguments().Count() == 3)
                .Single(
                    mi =>
                    {
                        var typeInfo = mi.GetParameters()[0].ParameterType.GenericTypeArguments[1];
                        return typeInfo.IsGenericType
                            && typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);
                    });

        internal static readonly MethodInfo ThenIncludeAfterReferenceMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(ThenInclude))
                .Single(
                    mi => mi.GetGenericArguments().Count() == 3
                        && mi.GetParameters()[0].ParameterType.GenericTypeArguments[1].IsGenericParameter);

        /// <summary>
        ///     Specifies additional related data to be further included based on a related type that was just included.
        /// </summary>
        /// <example>
        ///     <para>
        ///         The following query shows including a single level of related entities:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts)</code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch:
        ///     </para>
        ///     <code>
        /// context.Blogs
        ///    .Include(blog => blog.Posts).ThenInclude(post => post.Tags)
        ///     </code>
        ///     <para>
        ///         The following query shows including multiple levels and branches of related data:
        ///     </para>
        ///     <code>
        /// context.Blogs
        ///    .Include(blog => blog.Posts).ThenInclude(post => post.Tags).ThenInclude(tag => tag.TagInfo)
        ///    .Include(blog => blog.Contributors)
        ///     </code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch, second one being on derived type using
        ///         casting:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts).ThenInclude(post => ((SpecialPost)post).SpecialTags)</code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch, second one being on derived type using
        ///         the <see langword="as" /> operator.
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts).ThenInclude(post => (post as SpecialPost).SpecialTags)</code>
        /// </example>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <typeparam name="TPreviousProperty"> The type of the entity that was just included. </typeparam>
        /// <typeparam name="TProperty"> The type of the related entity to be included. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <param name="navigationPropertyPath">
        ///     A lambda expression representing the navigation property to be included (<c>t => t.Property1</c>).
        /// </param>
        /// <returns>
        ///     A new query with the related data included.
        /// </returns>
        public static IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
            => new IncludableQueryable<TEntity, TProperty>(
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: ThenIncludeAfterEnumerableMethodInfo.MakeGenericMethod(
                                typeof(TEntity), typeof(TPreviousProperty), typeof(TProperty)),
                            arguments: new[] { source.Expression, Expression.Quote(navigationPropertyPath) }))
                    : source);

        /// <summary>
        ///     Specifies additional related data to be further included based on a related type that was just included.
        /// </summary>
        /// <example>
        ///     <para>
        ///         The following query shows including a single level of related entities:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts)</code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts).ThenInclude(post => post.Tags)</code>
        ///     <para>
        ///         The following query shows including multiple levels and branches of related data:
        ///     </para>
        ///     <code>
        /// context.Blogs
        ///    .Include(blog => blog.Posts).ThenInclude(post => post.Tags).ThenInclude(tag => tag.TagInfo)
        ///    .Include(blog => blog.Contributors)
        ///     </code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch, second one being on derived type:
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts).ThenInclude(post => ((SpecialPost)post).SpecialTags)</code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch, second one being on derived type using
        ///         alternative method.
        ///     </para>
        ///     <code>context.Blogs.Include(blog => blog.Posts).ThenInclude(post => (post as SpecialPost).SpecialTags)</code>
        /// </example>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <typeparam name="TPreviousProperty"> The type of the entity that was just included. </typeparam>
        /// <typeparam name="TProperty"> The type of the related entity to be included. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <param name="navigationPropertyPath">
        ///     A lambda expression representing the navigation property to be included (<c>t => t.Property1</c>).
        /// </param>
        /// <returns>
        ///     A new query with the related data included.
        /// </returns>
        public static IIncludableQueryable<TEntity, TProperty> ThenInclude<TEntity, TPreviousProperty, TProperty>(
            this IIncludableQueryable<TEntity, TPreviousProperty> source,
            Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath)
            where TEntity : class
            => new IncludableQueryable<TEntity, TProperty>(
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: ThenIncludeAfterReferenceMethodInfo.MakeGenericMethod(
                                typeof(TEntity), typeof(TPreviousProperty), typeof(TProperty)),
                            arguments: new[] { source.Expression, Expression.Quote(navigationPropertyPath) }))
                    : source);

        private sealed class IncludableQueryable<TEntity, TProperty> : IIncludableQueryable<TEntity, TProperty>, IAsyncEnumerable<TEntity>
        {
            private readonly IQueryable<TEntity> _queryable;

            public IncludableQueryable(IQueryable<TEntity> queryable)
            {
                _queryable = queryable;
            }

            public Expression Expression
                => _queryable.Expression;

            public Type ElementType
                => _queryable.ElementType;

            public IQueryProvider Provider
                => _queryable.Provider;

            public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
                => ((IAsyncEnumerable<TEntity>)_queryable).GetAsyncEnumerator(cancellationToken);

            public IEnumerator<TEntity> GetEnumerator()
                => _queryable.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }

        internal static readonly MethodInfo StringIncludeMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethods(nameof(Include))
                .Single(
                    mi => mi.GetParameters().Any(
                        pi => pi.Name == "navigationPropertyPath" && pi.ParameterType == typeof(string)));

        /// <summary>
        ///     Specifies related entities to include in the query results. The navigation property to be included is
        ///     specified starting with the type of entity being queried (<typeparamref name="TEntity" />). Further
        ///     navigation properties to be included can be appended, separated by the '.' character.
        /// </summary>
        /// <example>
        ///     <para>
        ///         The following query shows including a single level of related entities:
        ///     </para>
        ///     <code>context.Blogs.Include("Posts")</code>
        ///     <para>
        ///         The following query shows including two levels of entities on the same branch:
        ///     </para>
        ///     <code>context.Blogs.Include("Posts.Tags")</code>
        ///     <para>
        ///         The following query shows including multiple levels and branches of related data:
        ///     </para>
        ///     <code>
        /// context.Blogs
        ///    .Include("Posts.Tags.TagInfo')
        ///    .Include("Contributors")
        ///     </code>
        /// </example>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <param name="navigationPropertyPath"> A string of '.' separated navigation property names to be included.  </param>
        /// <returns> A new query with the related data included. </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="source" /> or <paramref name="navigationPropertyPath" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="navigationPropertyPath" /> is empty or whitespace.
        /// </exception>
        public static IQueryable<TEntity> Include<TEntity>(
            this IQueryable<TEntity> source,
            [NotParameterized] string navigationPropertyPath)
            where TEntity : class
        {

            Check.NotEmpty(navigationPropertyPath, nameof(navigationPropertyPath));

            return
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: StringIncludeMethodInfo.MakeGenericMethod(typeof(TEntity)),
                            arg0: source.Expression,
                            arg1: Expression.Constant(navigationPropertyPath)))
                    : source;
        }

        #endregion

        #region Auto included navigations

        internal static readonly MethodInfo IgnoreAutoIncludesMethodInfo
            = typeof(EntityFrameworkQueryableExtensions)
                .GetTypeInfo().GetDeclaredMethod(nameof(IgnoreAutoIncludes));

        /// <summary>
        ///     Specifies that the current Entity Framework LINQ query should not have any
        ///     model-level eager loaded navigations applied.
        /// </summary>
        /// <typeparam name="TEntity"> The type of entity being queried. </typeparam>
        /// <param name="source"> The source query. </param>
        /// <returns>
        ///     A new query that will not apply any model-level eager loaded navigations.
        /// </returns>
        public static IQueryable<TEntity> IgnoreAutoIncludes<TEntity>(
            this IQueryable<TEntity> source)
            where TEntity : class
        {


            return
                source.Provider is EntityQueryProvider
                    ? source.Provider.CreateQuery<TEntity>(
                        Expression.Call(
                            instance: null,
                            method: IgnoreAutoIncludesMethodInfo.MakeGenericMethod(typeof(TEntity)),
                            arguments: source.Expression))
                    : source;
        }

        #endregion

    }
}
