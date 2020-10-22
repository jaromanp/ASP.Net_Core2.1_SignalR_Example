using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoNetCore2._1.Dominio.Contratos.Repositorios;
using DemoNetCore2._1.Dominio.Contratos.Servicios;
using DemoNetCore2._1.Dominio.Servicios;
using DemoNetCore2._1.Infraestructura.Repositorios;
using DemoNetCore2._1.Infraestructura.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DemoNetCore2._1.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
            services.AddScoped<IProductoServicio, ProductoServicio>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseSignalR(hub =>
            {
                hub.MapHub<TablaHub>("/api/Values");
            });
            app.UseMvc();
        }
    }
}
