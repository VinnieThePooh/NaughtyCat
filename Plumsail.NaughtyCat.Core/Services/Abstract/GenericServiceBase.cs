using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;

namespace Plumsail.NaughtyCat.Core.Services.Abstract
{
    public abstract class GenericServiceBase<TEntity, TDto> : IGenericService<TEntity, TDto, int>
        where TEntity : IHasKey<int>, new()
        where TDto : IDtoMarker
    {
        protected readonly IGenericProvider<TEntity, int> DataProvider;
        protected readonly IMapper Mapper;

        protected GenericServiceBase(IGenericProvider<TEntity, int> dataProvider, IMapper mapper)
        {
            DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public virtual async Task<int> Add(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);
            return await DataProvider.Add(entity);
        }

        public virtual async Task<TDto> GetByKey(int key)
        {
            var data = await DataProvider.GetByKey(key);
            return Mapper.Map<TDto>(data);
        }

        public virtual async Task Update(TDto dto)
        {
            var freshEntity = Mapper.Map<TEntity>(dto);
            await DataProvider.Update(freshEntity);
        }

        public virtual Task Delete(int key) => DataProvider.Delete(key);

        public async Task<List<TDto>> GetByCondition<TFilter>(TFilter filter) where TFilter : IFilterMarker
        {
            var data = await DataProvider.GetByCondition(GenerateExpression(filter));
            return data.Select(x => Mapper.Map<TDto>(x)).ToList();
        }

        public async Task<List<TDto>> GetByCondition<TFilter>(TFilter filter, int? pageNumber, int? pageSize)
            where TFilter : IFilterMarker
        {
            var pNumber = pageNumber ?? 1;

            //todo: set default pagesize in config
            int pSize = pageSize ?? 10;
            var data = await DataProvider.GetByCondition(GenerateExpression(filter), pNumber, pSize);
            return data.Select(x => Mapper.Map<TDto>(x)).ToList();
        }

        protected abstract Expression<Func<TEntity, bool>> GenerateExpression<TFilter>(TFilter filter)
            where TFilter : IFilterMarker;
    }
}