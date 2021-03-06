﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Common.Models;
using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;

namespace Plumsail.NaughtyCat.Core.Services.Abstract
{
    public abstract class GenericServiceBase<TEntity, TDto, TFilter> : IGenericService<TEntity, TDto, TFilter, int>
        where TEntity : IHasKey<int>, new()
        where TDto : IDtoMarker
		where TFilter: IFilterMarker
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

        // get paging model
		/// <summary>
		/// If pageSize is 0 - returns the whole dataset
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="orderingOptions"></param>
		/// <returns></returns>
        public async Task<PagingModel<TDto>> GetByCondition(TFilter filter, int pageNumber, int pageSize,
            OrderingOptions<TEntity, int> orderingOptions = null)
        {
            if (pageNumber < 1)
                throw new ArgumentException(nameof(pageNumber));

            if (pageSize < 0)
                throw new ArgumentException(nameof(pageSize));

			// this is sync operation anyway
            var data = DataProvider.GetByCondition(GenerateExpression(filter), orderingOptions).Result;
			
            var count = await data.CountAsync();

            if (pageSize == 0)
            {
                return new PagingModel<TDto>(count, count, 1)
                {
                    PageData = await data.Skip(0)
	                    .Take(count)
	                    .Select(x => Mapper.Map<TDto>(x))
	                    .ToArrayAsync()
                };
            }

            return new PagingModel<TDto>(count, pageSize, pageNumber)
            {
                PageData = await data.Skip((pageNumber - 1) * pageSize)
	                .Take(pageSize)
	                .Select(x => Mapper.Map<TDto>(x))
	                .ToArrayAsync()
            };
        }

        protected abstract Expression<Func<TEntity, bool>> GenerateExpression(TFilter filter);
    }
}