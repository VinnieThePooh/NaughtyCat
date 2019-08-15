using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.DataAccess.Providers.Abstract
{
    public interface IGenericProvider<TEntity, TKey>
        where TEntity : IHasKey<TKey>, new()
    {
        Task<TKey> Add(TEntity entity);
        Task<TEntity> GetByKey(TKey key);

        Task Update(TEntity entity);

        Task Delete(TKey key);

        Task<IQueryable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> filter);

        Task<IQueryable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> filter, int pageNumber, int pageSize);
    }
}
