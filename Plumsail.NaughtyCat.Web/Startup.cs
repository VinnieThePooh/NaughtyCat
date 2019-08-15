using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Core;
using Plumsail.NaughtyCat.Core.Mapping;
using Plumsail.NaughtyCat.DataAccess;
using Plumsail.NaughtyCat.Domain.Enums;
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
            services.AddAutoMapper(new Assembly[]
            {
                typeof(MappingProfile).Assembly
            });

            services.AddDbContext<NaughtyCatDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseOpenIddict<int>();
            });

            // identity services
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<NaughtyCatDbContext>()
                .AddDefaultTokenProviders();
            

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

                    var userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = provider.GetRequiredService<RoleManager<ApplicationRole>>();
                    await SeedIdentityData(userManager, roleManager);
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
            
            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseMvc();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                // todo: check why sometimes falls with timeout
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private async Task SeedIdentityData(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }


        // todo: probably refactor to grab roles automatically from somewhere
        private async Task SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var enumValue in Enum.GetValues(typeof(RoleEnum)).OfType<RoleEnum>())
            {
                var roleName = enumValue.GetEnumDescription();

                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole()
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper(),
                        CreateDate = DateTime.Now
                    });
                }
            }
        }

        private async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var email = "naughtycat@ncat.gov";
            var name = "Jake";
            var password = "ncPass12#";
            
            if (await userManager.FindByEmailAsync(email) == null)
            {
                await CreateUserWithRole(name, email, password, RoleEnum.Administrator, userManager);
            }

            email = "johndoe@gmail.com";
            name = "Johny";
            password = "doePass12#";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                await CreateUserWithRole(name, email, password, RoleEnum.BasicUser, userManager);
            }
        }


        private async Task CreateUserWithRole(string name, string email, string password, RoleEnum role,
            UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser()
            {
                UserName = name,
                NormalizedUserName = name.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                CreateDate = DateTime.Now,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user,password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role.GetEnumDescription());
            }
        }
    }
}