using System.Collections.Generic;

namespace Plumsail.NaughtyCat.Common.Models
{
    public class PagingModel<T>
    {
        public IEnumerable<T> PageData { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPagesCount { get; set; }

        public int TotalRecordsCount { get; set; }
    }
}
