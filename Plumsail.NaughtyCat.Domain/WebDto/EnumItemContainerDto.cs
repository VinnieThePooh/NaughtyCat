using System;
using System.Collections.Generic;
using System.Text;

namespace Plumsail.NaughtyCat.Domain.WebDto
{
    public class EnumItemContainerDto
    {
        public string EnumName { get; set; }

        public List<EnumItemDto> Items { get; set; } = new List<EnumItemDto>();
    }
}
