using System.Collections.Generic;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Domain.Models
{
    public class RabbitPriority: HandbookBase, IHasDescription
    {
        public string Description { get; set; }
        public virtual List<Rabbit> Rabbits { get; set; } = new List<Rabbit>();
    }
}
