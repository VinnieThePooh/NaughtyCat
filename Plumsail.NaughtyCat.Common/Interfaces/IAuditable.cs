using System;

namespace Plumsail.NaughtyCat.Common.Interfaces
{
    public interface IAuditable
    {
        DateTime? CreateDate { get; set; }

        DateTime? UpdateDate { get; set; }
    }
}