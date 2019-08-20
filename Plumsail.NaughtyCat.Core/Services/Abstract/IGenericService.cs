using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Common.Models;

namespace Plumsail.NaughtyCat.Core.Services.Abstract
{
    public interface IGenericService<TEntity, TDto, TKey> 
        where TEntity : IHasKey<TKey>
        where TDto : IDtoMarker
    {
        Task<TKey> Add(TDto dto);
        Task<TDto> GetByKey(TKey key);

        Task Update(TDto dto);

        Task Delete(TKey key);

        Task<PagingModel<TDto>> GetByCondition<TFilter>(TFilter filter, int pageNumber, int pageSize, OrderingOptions<TEntity, TKey> orderingOptions = null) where TFilter : IFilterMarker;
    }
}