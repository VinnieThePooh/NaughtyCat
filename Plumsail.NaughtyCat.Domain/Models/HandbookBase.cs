using System;
using System.Collections.Generic;
using System.Text;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Domain.Models
{
    public abstract class HandbookBase: IHasKey<int>
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
