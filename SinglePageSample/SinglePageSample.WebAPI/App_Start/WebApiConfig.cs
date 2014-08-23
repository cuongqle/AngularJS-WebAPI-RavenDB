using Microsoft.Practices.Unity;
using Raven.Client;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Db.RavenStore;
using SinglePageSample.Repository;
using SinglePageSample.Repository.Interfaces;
using SinglePageSample.WebAPI.Resolvers;
using System.Web.Http;

namespace SinglePageSample.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IDocumentProvider<IDocumentStore>, RavenDocumentProvider>(
                new TransientLifetimeManager());
            container.RegisterType<IDbStore, RavenDbStore>((
                new TransientLifetimeManager()),
                new InjectionConstructor(new ResolvedParameter<IDocumentProvider<IDocumentStore>>(), "Default"));

            container.RegisterType<ICompanyRepository, CompanyRepository>(new TransientLifetimeManager());
            container.RegisterType<IEmployeeRepository, EmployeeRepository>(new TransientLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
