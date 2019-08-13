using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.DataAccess.Repositories
{
    public abstract class GenericProviderBase<TEntity> : IGenericProvider<TEntity, int> where TEntity: class, IHasKey<int>
    {
        protected readonly NaughtyCatDbContext _dbContext;

        protected readonly DbSet<TEntity> _dataSet;

        protected GenericProviderBase(NaughtyCatDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dataSet = dbContext.Set<TEntity>();
        }

        public Task<int> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByCondition<TFilter>(Func<TEntity, bool> filter) where TFilter : IFilterMarker
        {
            throw new NotImplementedException();
        }
    }
}
