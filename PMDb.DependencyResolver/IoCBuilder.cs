using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PMDb.Domain.Core;
using PMDb.Domain.Interfaces;
using PMDb.Infrastructure.Data;
using PMDb.Services;


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

            builder.RegisterType<MovieRepository>()
                .As<IMovieRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FiltrationRepository>()
                .As<IFiltrationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FilterChecker>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<FilterTransformer>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<MovieFilters>()
                .AsSelf()
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

            builder.RegisterType<FiltrationService>()
                .As<IFiltrationService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SearchService>()
                .As<ISearchService>()
                .InstancePerLifetimeScope();

            container = builder.Build();

            BeginLifeTime();
        }

        private void BeginLifeTime()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<IConnectionStringProvider>();
                var context = scope.Resolve<MovieContext>();
                var mRepo = scope.Resolve<IMovieRepository>();
                var filters = scope.Resolve<MovieFilters>();
                scope.Resolve<FilterChecker>(new TypedParameter(typeof(MovieContext), context));
                scope.Resolve<FilterTransformer>(new TypedParameter(typeof(MovieFilters), filters));
                var fRepo = scope.Resolve<IFiltrationRepository>();
                //scope.Resolve<IFiltrationService>();
            }
        }

        public AutofacServiceProvider GetContainer()
        {
            return new AutofacServiceProvider(container);
        }

    }
}
