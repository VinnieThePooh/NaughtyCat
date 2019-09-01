using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Common.Models;

namespace Plumsail.NaughtyCat.DataAccess.Providers.Abstract
{
    public interface IGenericProvider<TEntity, TKey>
        where TEntity : IHasKey<TKey>, new()
    {
        Task<TKey> Add(TEntity entity);
        Task<TEntity> GetByKey(TKey key);

        Task Update(TEntity entity);

        Task Delete(TKey key);

        Task<IOrderedQueryable<TEntity>> GetByCondition(Func<TEntity, bool> filter, OrderingOptions<TEntity, TKey> orderingOptions = null);
    }
}
