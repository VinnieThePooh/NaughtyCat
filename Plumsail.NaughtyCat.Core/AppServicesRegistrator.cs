using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Core.Services;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.Jwt;
using Plumsail.NaughtyCat.Domain.Models.ListViews;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core
{
    public class AppServicesRegistrator: IServicesRegistrator
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _config;

        public AppServicesRegistrator(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            _config = config;
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.Configure<JwtTokenModel>(options => _config.GetSection("TokenManagement").Bind(options));

            services.AddTransient<GenericServiceBase<Rabbit, RabbitDto, RabbitListModelFilter>, RabbitsService>();
            services.AddTransient<IAuthService, TokenAuthenticationService>();
        }
    }
}
