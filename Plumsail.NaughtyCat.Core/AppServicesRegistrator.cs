using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Core.Services;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.Jwt;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core
{
    public class AppServicesRegistrator: IServicesRegistrator
    {
        private readonly IConfiguration _config;

        public AppServicesRegistrator(IConfiguration config)
        {
            _config = config;
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.Configure<JwtTokenModel>(options => _config.GetSection("TokenManagement").Bind(options));

            services.AddTransient<GenericServiceBase<Rabbit, RabbitDto>, RabbitsService>();
            services.AddTransient<IAuthService, TokenAuthenticationService>();
        }
    }
}
