using System;
using AutoMapper;
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Domain.Enums;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.Core.Mapping
{
    public class DelicacyStringToClassConverter : IValueConverter<string, RabbitDelicacy>
    {
        public RabbitDelicacy Convert(string sourceMember, ResolutionContext context)
        {
            if (!Enum.TryParse(sourceMember, true, out DelicacyEnum result))
                return null;

            var d = result.GetEnumDescription();
            return new RabbitDelicacy()
            {
                Name = d,
                Id = (int) result,
                Description = d,
            };
        }
    }
}