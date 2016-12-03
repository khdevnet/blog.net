using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Blog.Web.Application.Infrastructure;
using Blog.Web.Application.Service;
using Blog.Web.Application.Service.Entity;
using Blog.Web.Application.Service.Internal;
using Blog.Web.Application.Storage;
using Blog.Web.Application.Storage.Json;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Blog.Web
{
    public class ContainerConfig
    {
        public const string JsonRepositoryType = "json";

        public static void SetUpContainer()
        {
            var container = RegisterDependencies();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // replace the default FilterAttributeFilterProvider with one that has Autofac property
            // injection
            FilterProviders.Providers.Remove(FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider));
            FilterProviders.Providers.Add(new AutofacFilterProvider());
        }

        private static ResolvedParameter GetResolvedParameterByName<T>(string key)
        {
            return new ResolvedParameter(
                (pi, c) => pi.ParameterType == typeof(T),
                (pi, c) => c.ResolveNamed<T>(key));
        }

        private static IContainer RegisterDependencies()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<ThemeableRazorViewEngine>().As<IViewEngine>().InstancePerLifetimeScope();

            var repositoryKeys = new RepositoryKeys();
            repositoryKeys.Add<Entry>(e => e.Slug);
            repositoryKeys.Add<About>(a => a.Title);
            repositoryKeys.Add<Home>(a => a.Title);
            repositoryKeys.Add<Config>(c => c.Site);
            repositoryKeys.Add<User>(u => u.Id);
            repositoryKeys.Add<Image>(i => i.FileName);

            builder.RegisterType<JsonRepository>().Named<IRepository>(JsonRepositoryType)
                .InstancePerLifetimeScope()
                .WithParameters(new[] {
                    new NamedParameter("keys", repositoryKeys)
                });

            builder.RegisterControllers(typeof(ContainerConfig).Assembly)
                .WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());

            builder.RegisterType<ConfigService>().As<IConfigService>().InstancePerLifetimeScope()
                .WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));

            builder.RegisterType<EntryService>().As<IEntryService>().InstancePerLifetimeScope()
                .WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));

            builder.RegisterType<AboutService>().As<IAboutService>().InstancePerLifetimeScope()
                .WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));

            builder.RegisterType<HomeService>().As<IHomeService>().InstancePerLifetimeScope()
            .WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));

            builder.RegisterType<ImageService>().As<IImageService>().InstancePerLifetimeScope()
                .WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));//azureblob

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().WithParameter(GetResolvedParameterByName<IRepository>(JsonRepositoryType));
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<ThemeService>().As<IThemeService>().InstancePerLifetimeScope();
            builder.RegisterType<Services>().As<IServices>().InstancePerLifetimeScope();

            var container = builder.Build();
            return container;
        }
    }
}