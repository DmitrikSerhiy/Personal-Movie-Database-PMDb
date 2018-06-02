using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using PMDb.DependencyResolver;
using PMDb.Services;
using PMDb.Services.Mappers;

namespace PMDb.API
{
    
    public class Startup
    {
        private IoCBuilder IoCBuilder;
        private MapperInitializatior mapperInitializatior;
        public Startup()
        {
            
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AngularFrontEnd",
                    builder => builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader());
            });

            services.AddMvc();
            IoCBuilder = new IoCBuilder(services);
            mapperInitializatior = new MapperInitializatior();
            return IoCBuilder.GetContainer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AngularFrontEnd");

            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "./src/index.html";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
