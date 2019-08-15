using Microsoft.Extensions.DependencyInjection;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Core.Services;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core
{
    public class AppServicesRegistrator: IServicesRegistrator
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<GenericServiceBase<Rabbit, RabbitDto>, RabbitsService>();
        }
    }
}
