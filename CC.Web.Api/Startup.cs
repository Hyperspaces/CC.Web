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
using System.Reflection;

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

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
