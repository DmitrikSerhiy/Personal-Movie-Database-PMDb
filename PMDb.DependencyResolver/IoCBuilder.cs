using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using PMDb.Domain.Interfaces;
using PMDb.Infrastructure.Data;
using PMDb.Services;
using System;

namespace PMDb.DependencyResolver
{
    public class IoCBuilder
    {
        private Autofac.IContainer container;
        public IoCBuilder(IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                .ActionContext;
                return new UrlHelper(actionContext);
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            //builder.RegisterType<ActionContextAccessor>()
            //    .As<IActionContextAccessor>()
            //    .SingleInstance();

            //builder.RegisterType<UrlHelper>()
            //    .As<IUrlHelper>()
            //    .InstancePerLifetimeScope();

            builder.RegisterType<MovieRepository>()
                .As<IMovieRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConnectionStringProvider>()
                .As<IConnectionStringProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MovieContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<MovieService>()
                .As<IMovieService>()
                .InstancePerLifetimeScope();

            container = builder.Build();

            BeginLifeTime();
        }

        private void BeginLifeTime()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var actionContext = scope.Resolve<IActionContextAccessor>().ActionContext;

                // var ac = new ActionContextAccessor().ActionContext;
                //var urlhelper = scope.Resolve<IUrlHelper>(
                //    new TypedParameter(typeof(IActionContextAccessor), new ActionContextAccessor().ActionContext));

                var repo = scope.Resolve<IMovieRepository>();
                var CSProvider = scope.Resolve<IConnectionStringProvider>();
                var context = scope.Resolve<MovieContext>();
            }
        }

        public AutofacServiceProvider GetContainer()
        {
            return new AutofacServiceProvider(container);
        }

    }
}
