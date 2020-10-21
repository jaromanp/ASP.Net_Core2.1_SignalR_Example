using DemoNetCore2._1.Dominio.Contratos.Repositorios;
using DemoNetCore2._1.Dominio.Contratos.Servicios;
using DemoNetCore2._1.Dominio.Servicios;
using DemoNetCore2._1.Infraestructura.Hubs;
using DemoNetCore2._1.Infraestructura.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DemoNetCore2._1.Presentacion
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment environment)
        {
            this._environment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddSignalR();

            #region IOC
            services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
            services.AddScoped<IProductoServicio, ProductoServicio>();
            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSignalR(hub =>
            {
                hub.MapHub<TablaHub>("/tablaHub");
            });
            app.UseMvc(path =>
            {
                path.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}