using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.DataAccess;
using Plumsail.NaughtyCat.DataAccess.Repositories;

namespace Plumsail.NaughtyCat.Core.Services
{
    public abstract class GenericServiceBase<TEntity, TDto> : IGenericService<TEntity, TDto, int> where TEntity: IHasKey<int>
        where TDto: IDtoMarker
    {
        protected readonly IGenericProvider<TEntity, int> _dataProvider;

        protected GenericServiceBase(IGenericProvider<TEntity, int> dataProvider)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }
        public Task<int> Add(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        public Task Update(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetByCondition<TFilter>(Func<TDto, bool> filter) where TFilter : IFilterMarker
        {
            throw new NotImplementedException();
        }
    }
}
