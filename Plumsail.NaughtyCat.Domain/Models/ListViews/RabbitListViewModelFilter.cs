using System;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Domain.Enums;

namespace Plumsail.NaughtyCat.Domain.Models.ListViews
{
    public class RabbitListModelFilter : IFilterMarker
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Color { get; set; }
        public DelicacyEnum Delicacy { get; set; }
        public PriorityEnum Priority { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public DateTime? UpdateDateFrom { get; set; }
        public DateTime? UpdateDateTo { get; set; }
    }
}