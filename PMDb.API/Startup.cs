using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddMvc();
            IoCBuilder = new IoCBuilder(services);
            mapperInitializatior = new MapperInitializatior();
            return IoCBuilder.GetContainer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }
    }
}
