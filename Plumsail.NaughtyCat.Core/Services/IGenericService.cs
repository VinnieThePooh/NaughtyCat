using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Core.Services
{
    public interface IGenericService<TEntity, TDto, TKey> 
        where TEntity : IHasKey<TKey>
        where TDto : IDtoMarker
    {
        Task<TKey> Add(TDto dto);
        Task<TDto> GetByKey(TKey key);

        Task Update(TDto dto);

        Task Delete(TKey key);

        Task<List<TDto>> GetByCondition<TFilter>(Func<TDto, bool> filter) where TFilter : IFilterMarker;
    }
}