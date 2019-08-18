using AutoMapper;
using Plumsail.NaughtyCat.Common.Constants;
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Domain.Enums;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // todo: simplify
            CreateMap<Rabbit, RabbitDto>().ForMember(x => x.Delicacy,
                    c => c.MapFrom(r => r.IdRabbitDelicacy))
                .ForMember(x => x.Priority, c => c.MapFrom(r => r.IdRabbitPriority));

            CreateMap<RabbitDto, Rabbit>()
                .ForMember(x => x.IdRabbitDelicacy, c => c.MapFrom(x => x.Delicacy))
                .ForMember(x => x.IdRabbitPriority, c => c.MapFrom(x => x.Priority))
                .ForMember(x => x.Priority, c => c.Ignore())
                .ForMember(x => x.Delicacy, c => c.Ignore());
        }
    }
}