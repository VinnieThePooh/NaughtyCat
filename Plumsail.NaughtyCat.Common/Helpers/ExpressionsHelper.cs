using System;
using System.Linq.Expressions;
using System.Reflection;
using Plumsail.NaughtyCat.Common.Interfaces;
// ReSharper disable All

namespace Plumsail.NaughtyCat.Common.Helpers
{
	public static class ExpressionsHelper
	{

		public const string FilterParameterName = "filter";
		public const string EntityParameterName = "entity";

		public static BinaryExpression TrueExpression =>
			Expression.Equal(Expression.Constant(0), Expression.Constant(0));

		public static BinaryExpression FalseExpression =>
			Expression.Equal(Expression.Constant(0), Expression.Constant(1));

		public static BinaryExpression IsNull(MemberExpression memberExpression) =>
			Expression.Equal(memberExpression, Expression.Constant(null, memberExpression.Type));

		public static BinaryExpression IsNotNull(MemberExpression memberExpression) =>
			Expression.NotEqual(memberExpression, Expression.Constant(null, memberExpression.Type));

		public static BinaryExpression HasValue(MemberExpression memberExpression)
		{
			var hasValueExpression = Expression.Property(memberExpression, "HasValue");
			return Expression.Equal(hasValueExpression, Expression.Constant(true));
		}

		public static BinaryExpression HasNoValue(MemberExpression memberExpression)
		{
			var hasValueExpression = Expression.Property(memberExpression, "HasValue");
			return Expression.NotEqual(hasValueExpression, Expression.Constant(true));
		}

		public static BinaryExpression Contains<TEntity, TFilter>(string propName) where TFilter: IFilterMarker
		{
			var parExpr = Expression.Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Expression.Parameter(typeof(TFilter), FilterParameterName);

			var entityProp = Expression.Property(parExpr, propName);
			var filterProp = Expression.Property(filterExpr, propName);

			var nullOrEmptyMi = typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Static | BindingFlags.Public);

			var containsMi = typeof(string).GetMethod("Contains", BindingFlags.Public | BindingFlags.Instance, null, new Type[] {typeof(string)}, null);


			var nullOrEmptyFilterExpr = Expression.Call(nullOrEmptyMi, filterProp);
			
			var notNullOrEmptyEntityExpr = Expression.NotEqual(Expression.Call(nullOrEmptyMi, entityProp), Expression.Constant(true));
			var containsExpr = Expression.AndAlso(notNullOrEmptyEntityExpr,
				Expression.Call(entityProp, containsMi, filterProp));

			return Expression.OrElse(nullOrEmptyFilterExpr, containsExpr);
		}

		public static BinaryExpression IsGreaterThanOrEqual<TEntity, TFilter>(string propName, string entityPropName) where TFilter : IFilterMarker
		{
			var parExpr = Expression.Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Expression.Parameter(typeof(TFilter), FilterParameterName);

			var entityProp = Expression.Property(parExpr, entityPropName);
			var filterProp = Expression.Property(filterExpr, propName);

			return Expression.OrElse(HasNoValue(filterProp),
				Expression.AndAlso(HasValue(entityProp), Expression.GreaterThanOrEqual(entityProp, filterProp)));
		}

		public static BinaryExpression IsLessThanOrEqual<TEntity, TFilter>(string filterPropName, string entityPropName) where TFilter : IFilterMarker
		{
			var parExpr = Expression.Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Expression.Parameter(typeof(TFilter), FilterParameterName);

			var entityProp = Expression.Property(parExpr, entityPropName);
			var filterProp = Expression.Property(filterExpr, filterPropName);

			return Expression.OrElse(HasNoValue(filterProp),
				Expression.AndAlso(HasValue(entityProp), Expression.LessThanOrEqual(entityProp, filterProp)));
		}

		// enum or int types
		public static BinaryExpression Equals<TEntity, TFilter>(string  propName) where TFilter : IFilterMarker
		{
			var parExpr = Expression.Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Expression.Parameter(typeof(TFilter), FilterParameterName);

			var entityProp = Expression.Property(parExpr, propName);
			var filterProp = Expression.Property(filterExpr, propName);

			return Expression.OrElse(HasNoValue(filterProp), Expression.Equal(filterProp, entityProp));

		}

		public static Expression<Func<TEntity, bool>> ConvertToLambda<TEntity>(BinaryExpression expression)
		{
			var param = Expression.Parameter(typeof(TEntity), EntityParameterName);
			return Expression.Lambda<Func<TEntity, bool>>(expression, new[] {param});
		}
	}
}