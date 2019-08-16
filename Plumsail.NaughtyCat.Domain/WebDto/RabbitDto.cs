using System;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Domain.WebDto
{
    public class RabbitDto : IAuditable, IDtoMarker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public int Age { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Delicacy { get; set; }

        public string Priority { get; set; }
    }
}