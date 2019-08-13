using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.DataAccess.Repositories
{
    public interface IGenericProvider<TEntity, TKey>
        where TEntity : IHasKey<TKey>
    {
        Task<TKey> Add(TEntity entity);
        Task<TEntity> GetByKey(TKey key);

        Task Update(TEntity entity);

        Task Delete(TKey key);

        Task<IEnumerable<TEntity>> GetByCondition<TFilter>(Func<TEntity, bool> filter) where TFilter : IFilterMarker;
    }
}
