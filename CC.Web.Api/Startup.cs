using Autofac;
using Autofac.Extensions.DependencyInjection;
using CC.Web.Api.Core;
using CC.Web.Api.Filter;
using CC.Web.Dao;
using CC.Web.Dao.Core;
using CC.Web.Service.System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CC.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRedisClientsManager>(new BasicRedisClientManager());
            services.AddMvc(option => option.Filters.Add(typeof(ActionLogFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddDbContext<CCDbContext>(option => 
            option.UseSqlServer(Configuration.GetConnectionString("CCDatabase"), b => b.MigrationsAssembly("CC.Web.Api")));

            // 清除JWT映射关系（会修改返回的token）
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ClientId = "CCWebClient";
                    options.ClientSecret = "CCWebSecret";
                    options.SaveTokens = true;
                    options.ResponseType = "code"; ;

                    options.Scope.Clear();

                    options.Scope.Add("api1");
                    options.Scope.Add(IdentityServerConstants.StandardScopes.OpenId);
                    options.Scope.Add(IdentityServerConstants.StandardScopes.Profile);
                    options.Scope.Add("roles");

                    options.Scope.Add(OidcConstants.StandardScopes.OfflineAccess);

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                    };

                });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IUserService, UserService>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            AutofacConfig.RegisterObj(containerBuilder);
            var applicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
    }
}
