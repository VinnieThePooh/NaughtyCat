using Microsoft.Extensions.DependencyInjection;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.DataAccess.Providers;
using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess
{
    public class DataAccessRegistrator: IServicesRegistrator
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IGenericProvider<Rabbit, int>, RabbitsProvider>();
        }
    }
}
