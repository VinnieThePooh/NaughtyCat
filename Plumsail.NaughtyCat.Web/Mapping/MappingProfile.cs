using AutoMapper;
using Plumsail.NaughtyCat.Common.Constants;
using Plumsail.NaughtyCat.Common.Enums;
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // todo: simplify
            CreateMap<Rabbit, RabbitDto>().ForMember(x => x.Delicacy,
                c => c.MapFrom(r =>
                    r.IdRabbitDelicacy != null
                        ? ((DelicacyEnum) r.IdRabbitDelicacy).GetEnumDescription()
                        : StringConstants.NotAvailable))
            .ForMember(x => x.Priority, c => c.MapFrom(r => r.IdRabbitPriority != null
                ? ((DelicacyEnum) r.IdRabbitPriority).GetEnumDescription()
                : StringConstants.NotAvailable));

            CreateMap<RabbitDto, Rabbit>()
                .ForMember(x => x.Delicacy, c => c.ConvertUsing(new DelicacyStringToClassConverter()))
                .ForMember(x => x.Priority, c => c.ConvertUsing(new PriorityStringToClassConverter()));
        }
    }
}