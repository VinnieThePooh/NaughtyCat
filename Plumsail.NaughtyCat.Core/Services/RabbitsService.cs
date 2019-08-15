using System;
using System.Diagnostics;
using System.Linq.Expressions;
using AutoMapper;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core.Services
{
    public class RabbitsService: GenericServiceBase<Rabbit, RabbitDto>
    {
        public RabbitsService(IGenericProvider<Rabbit, int> dataProvider, IMapper mapper) : base(dataProvider, mapper)
        {
        }

        protected override Expression<Func<Rabbit, bool>> GenerateExpression<TFilter>(TFilter filter)
        {
            Debugger.Break();

            return null;
        }
    }
}
