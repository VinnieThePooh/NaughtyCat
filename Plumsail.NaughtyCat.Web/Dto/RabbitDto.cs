using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Web.Dto
{
    public class RabbitDto : IAuditable, IDtoMarker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Delicacy { get; set; }

        public string Priority { get; set; }
    }
}