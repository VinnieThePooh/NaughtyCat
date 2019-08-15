using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.DataAccess.Repositories;

namespace Plumsail.NaughtyCat.Core.Services
{
    public abstract class GenericServiceBase<TEntity, TDto> : IGenericService<TEntity, TDto, int> where TEntity: IHasKey<int>
        where TDto: IDtoMarker
    {
        protected readonly IGenericProvider<TEntity, int> _dataProvider;
        protected readonly IMapper _mapper;

        protected GenericServiceBase(IGenericProvider<TEntity, int> dataProvider, IMapper mapper)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<int> Add(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            return await _dataProvider.Add(entity);
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
