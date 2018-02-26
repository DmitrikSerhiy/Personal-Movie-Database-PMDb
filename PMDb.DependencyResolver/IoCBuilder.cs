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
            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<ActionContextAccessor>()
                .As<IActionContextAccessor>()
                .SingleInstance();

            builder.RegisterType<UrlHelper>()
                .As<IUrlHelper>()
                .UsingConstructor(typeof(IActionContextAccessor))
                .InstancePerLifetimeScope();

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

                // scope.Register(x => new UrlHelper(actionContext));
                //var uh = new UrlHelper(actionContext);
                var urlhelper = scope.Resolve<IUrlHelper>();
                //resolve in correct way

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
