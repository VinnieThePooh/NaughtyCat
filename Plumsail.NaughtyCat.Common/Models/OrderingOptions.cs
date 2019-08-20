using System;
using System.Linq.Expressions;
using Plumsail.NaughtyCat.Common.Enums;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Common.Models
{
	public class OrderingOptions<TEntity, TKey> where TEntity: IHasKey<TKey>
	{
		public Expression<Func<TEntity, object>> Order { get; set; } = x => x.Id;

		public OrderingDirection Direction { get; set; } = OrderingDirection.Descend;

		public static OrderingOptions<TEntity, TKey> Create() => new OrderingOptions<TEntity, TKey>();
	}
}
