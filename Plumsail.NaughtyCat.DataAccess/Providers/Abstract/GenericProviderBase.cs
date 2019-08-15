using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.DataAccess.Providers.Abstract
{
    public abstract class GenericProviderBase<TEntity> : IGenericProvider<TEntity, int>
        where TEntity : class, IHasKey<int>, new()
    {
        protected readonly NaughtyCatDbContext _dbContext;

        protected readonly DbSet<TEntity> _dataSet;

        protected GenericProviderBase(NaughtyCatDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dataSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<int> Add(TEntity entity)
        {
            var entry = await _dataSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public virtual Task<TEntity> GetByKey(int key) => _dataSet.FindAsync(key);


        public virtual async Task Update(TEntity entity)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(int key)
        {
            _dbContext.Remove(new TEntity {Id = key});
            await _dbContext.SaveChangesAsync();
        }

        public virtual Task<IQueryable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> filter, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException(nameof(pageNumber));

            if (pageSize <= 0)
                throw new ArgumentException(nameof(pageSize));

            var data = _dataSet.AsQueryable();

            if (filter != null)
            {
                data = data.Where(filter).AsQueryable();
            }

            return Task.FromResult(data.Skip((pageNumber - 1) * pageSize).Take(pageSize));
        }

        public virtual Task<IQueryable<TEntity>> GetByCondition(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
                return Task.FromResult(_dataSet.AsQueryable());

            return Task.FromResult(_dataSet.Where(filter).AsQueryable());
        }
    }
}