using Microsoft.Extensions.DependencyInjection;

namespace Plumsail.NaughtyCat.Common.Interfaces
{
    public interface IServicesRegistrator
    {
        void RegisterServices(IServiceCollection services);
    }
}
