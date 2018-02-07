using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PMDb.Domain.Interfaces;
using PMDb.Infrastructure.Data;
using System;
using System.ComponentModel;

namespace PMDb.DependencyResolver.IoC
{
    public class IoCBuilder
    {
        private Autofac.IContainer container;
        public IoCBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<MovieRepository>()
                .As<IMovieRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ConnectionStringProvider>()
                .As<IConnectionStringProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MovieContext>()
                .AsSelf()
                .InstancePerLifetimeScope();


            container = builder.Build();

            BeginLifeTime();
        }

        private void BeginLifeTime()
        {
            using (var scope = container.BeginLifetimeScope())
            {
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
