using System;
using System.Linq.Expressions;
using System.Reflection;
using Plumsail.NaughtyCat.Common.Interfaces;
using static System.Linq.Expressions.Expression;

namespace Plumsail.NaughtyCat.Common.Helpers
{
	public static class ExpressionsHelper
	{
		public const string FilterVariableName = "filter";
		public const string EntityParameterName = "entity";

		public static BinaryExpression TrueExpression =>
			Equal(Constant(0), Constant(0));

		public static BinaryExpression FalseExpression =>
			Equal(Constant(0), Constant(1));

		public static BinaryExpression IsNull(MemberExpression memberExpression) =>
			Equal(memberExpression, Constant(null, memberExpression.Type));

		public static BinaryExpression IsNotNull(MemberExpression memberExpression) =>
			NotEqual(memberExpression, Constant(null, memberExpression.Type));

		public static BinaryExpression HasValue(MemberExpression memberExpression)
		{
			var hasValueExpression = Property(memberExpression, "HasValue");
			return Equal(hasValueExpression, Constant(true));
		}

		public static BinaryExpression HasNoValue(MemberExpression memberExpression)
		{
			var hasValueExpression = Property(memberExpression, "HasValue");
			return NotEqual(hasValueExpression, Constant(true));
		}

		public static BinaryExpression Contains<TEntity, TFilter>(string propName) where TFilter : IFilterMarker
		{
			var parExpr = Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Variable(typeof(TFilter), FilterVariableName);

			var entityProp = Property(parExpr, propName);
			var filterProp = Property(filterExpr, propName);

			var nullOrEmptyMi = typeof(string).GetMethod("IsNullOrEmpty", BindingFlags.Static | BindingFlags.Public);

			var containsMi = typeof(string).GetMethod("Contains", BindingFlags.Public | BindingFlags.Instance, null,
				new Type[] {typeof(string)}, null);


			var nullOrEmptyFilterExpr = Call(nullOrEmptyMi, filterProp);

			var notNullOrEmptyEntityExpr = NotEqual(Call(nullOrEmptyMi, entityProp), Constant(true));
			var containsExpr = AndAlso(notNullOrEmptyEntityExpr,
				Call(entityProp, containsMi, filterProp));

			return OrElse(nullOrEmptyFilterExpr, containsExpr);
		}

		public static BinaryExpression IsGreaterThanOrEqual<TEntity, TFilter>(string propName, string entityPropName)
			where TFilter : IFilterMarker
		{
			var parExpr = Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Variable(typeof(TFilter), FilterVariableName);

			var entityProp = Property(parExpr, entityPropName);
			var filterProp = Property(filterExpr, propName);

			return OrElse(HasNoValue(filterProp),
				AndAlso(HasValue(entityProp),
					GreaterThanOrEqual(Property(entityProp, "Value"), Property(filterProp, "Value"))));
		}

		public static BinaryExpression IsLessThanOrEqual<TEntity, TFilter>(string filterPropName, string entityPropName)
			where TFilter : IFilterMarker
		{
			var parExpr = Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Variable(typeof(TFilter), FilterVariableName);

			var entityProp = Property(parExpr, entityPropName);
			var filterProp = Property(filterExpr, filterPropName);

			return OrElse(HasNoValue(filterProp),
				AndAlso(HasValue(entityProp),
					LessThanOrEqual(Property(entityProp, "Value"), Property(filterProp, "Value"))));
		}

		// enum? or int? types
		public static BinaryExpression Equals<TEntity, TFilter>(string propName, string entityPropName)
			where TFilter : IFilterMarker
		{
			var parExpr = Parameter(typeof(TEntity), EntityParameterName);
			var filterExpr = Variable(typeof(TFilter), FilterVariableName);

			var entityProp = Property(parExpr, entityPropName);
			var filterProp = Property(filterExpr, propName);

			UnaryExpression filterConverted = null;

			if (!entityProp.Type.IsAssignableFrom(filterProp.Type))
			{
				filterConverted = Convert(filterProp, entityProp.Type);
			}


			//var expression = filterConverted != null ? filterConverted: filterProp;

			if (filterConverted != null)
				return OrElse(HasNoValue(filterProp),
					Equal(Property(filterConverted, "Value"), Property(entityProp, "Value")));

			return OrElse(HasNoValue(filterProp), Equal(Property(filterProp, "Value"), Property(entityProp, "Value")));
		}

		public static Expression<Func<TEntity, bool>> ConvertToLambda<TEntity, TFilter>(BinaryExpression expression,
			TFilter filter) where TFilter : IFilterMarker
		{
			var param = Parameter(typeof(TEntity), EntityParameterName);
			var variable = Variable(typeof(TFilter), FilterVariableName);
			var assignExpression = Assign(variable, Constant(filter));

			var block = Block(
				new[] {variable, param},
				assignExpression,
				expression
			);

			return Lambda<Func<TEntity, bool>>(param);
		}
	}
}