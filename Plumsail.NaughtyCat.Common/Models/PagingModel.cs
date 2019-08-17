using System;
using System.Collections.Generic;

namespace Plumsail.NaughtyCat.Common.Models
{
    public class PagingModel<T>
    {
        private int _totalRecordsCount;
        private int _pageSize;

        public PagingModel(int totalRecordsCount, int pageSize)
        {
           ValidateTotalRecordsCount(totalRecordsCount);
           ValidatePageSize(pageSize);
           _totalRecordsCount = totalRecordsCount;
           _pageSize = pageSize;
           ChangeTotalPagesCount(totalRecordsCount, pageSize);
        }

        public IEnumerable<T> PageData { get; set; }

        public int PageNumber { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                ValidatePageSize(value);
                _pageSize = value;
                ChangeTotalPagesCount(_totalRecordsCount, value);
            }
        }

        public int TotalPagesCount { get; private set; }

        public int TotalRecordsCount
        {
            get => _totalRecordsCount;
            set
            {
                ValidateTotalRecordsCount(value);
                _totalRecordsCount = value;
                ChangeTotalPagesCount(value, _pageSize);
            }
        }

        #region Implementation details

        private void ChangeTotalPagesCount(int tRecordsCount, int pSize)
        {
            if (tRecordsCount != 0)
            {
                TotalPagesCount = (int)Math.Ceiling((double)tRecordsCount / pSize);
            }
            else
            {
                TotalPagesCount = 0;
            }
        }

        private void ValidatePageSize(int pageSize)
        {
            if (pageSize <= 0)
                throw new ArgumentException(nameof(pageSize));
        }

        private void ValidateTotalRecordsCount(int tRecordsCount)
        {
            if (tRecordsCount < 0)
                throw new ArgumentException(nameof(tRecordsCount));
        }

        #endregion
    }
}
