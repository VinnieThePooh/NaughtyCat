using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Plumsail.NaughtyCat.Core;
using Plumsail.NaughtyCat.DataAccess;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NaughtyCatDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict<int>();
            });

            // identity services
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<NaughtyCatDbContext>()
                .AddDefaultTokenProviders();

            // OpenIddict services
            //services
            //    .AddOpenIddict()
            //    .AddCore(options =>
            //    {
            //        options.UseEntityFrameworkCore().UseDbContext<NaughtyCatDbContext>()
            //            .ReplaceDefaultEntities<int>();
            //    })
            //    .AddServer(options =>
            //    {
            //        // Enable the token endpoint (required to use the password flow and code flow).
            //        options.EnableTokenEndpoint("/connect/token")
            //            .EnableAuthorizationEndpoint("/connect/authorize");

            //        // Allow client applications to use the grant_type=password flow.
            //        options.AllowPasswordFlow();

            //        options.AllowAuthorizationCodeFlow();

            //        // During development, you can disable the HTTPS requirement. Need or not?
            //        options.DisableHttpsRequirement();

            //        // Accept token requests that don't specify a client_id.
            //        options.AcceptAnonymousClients();
            //    }).AddValidation();

            // replace with OpenIddict?
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://localhost:44388",
                        ValidAudience = "https://localhost:44388",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ncatsecretKey@567"))
                    };
                });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(opt =>
            {
                // todo: can be simplified probably
                opt.AddPolicy("EnableCORS",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                    });
            });


            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            new DataAccessRegistrator().RegisterServices(services);
            new AppServicesRegistrator().RegisterServices(services);


            var scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var dbContext = provider.GetRequiredService<NaughtyCatDbContext>())
                {
                    await dbContext.Database.EnsureCreatedAsync();

                    // openiddict related
                    //var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

                    //var clientId = Configuration["OpenIDDict:ClientId"];

                    //if (await manager.FindByClientIdAsync(clientId) == null)
                    //{
                    //    var descriptor = new OpenIddictApplicationDescriptor
                    //    {
                    //        ClientId =  clientId,
                    //        ClientSecret = Configuration["OpenIDDict:ClientSecret"],
                    //        //RedirectUris = { new Uri(Configuration["OpenIDDict:RedirectUris"]) }
                    //    };

                    //    await manager.CreateAsync(descriptor);
                    //}
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseMvc();

            app.UseHttpsRedirection();
            app.UseCors("EnableCORS");
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                // todo: check why sometimes falls with timeout
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}