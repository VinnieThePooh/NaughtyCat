using System;
using AutoMapper;
using Plumsail.NaughtyCat.Common.Enums;
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.Web.Mapping
{
    // todo: make generic with enum parameter type
    public class PriorityStringToClassConverter : IValueConverter<string, RabbitPriority>
    {
        public RabbitPriority Convert(string sourceMember, ResolutionContext context)
        {
            if (!Enum.TryParse(sourceMember, true, out PriorityEnum result))
                return null;

            var d = result.GetEnumDescription();
            return new RabbitPriority()
            {
                Name = d,
                Id = (int)result,
                Description = d,
            };
        }
    }
}
