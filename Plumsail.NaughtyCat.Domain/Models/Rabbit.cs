using System;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Domain.Models
{
    public class Rabbit : IHasKey<int>, IAuditable
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Color { get; set; }

        public int? IdRabbitPriority { get; set; }
        public virtual RabbitPriority Priority { get; set; }

        public int? IdRabbitDelicacy { get; set; }
        public virtual RabbitDelicacy Delicacy { get; set; }
    }
}