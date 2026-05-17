using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Db.RavenStore;
using SinglePageSample.Repository;
using SinglePageSample.Repository.Interfaces;
using System;

namespace SinglePageSample.UnitTest.Bootstrappers
{
    public static class HotSpot
    {
        private static ServiceProvider ServiceProvider { get; set; }

        public static void WireUp()
        {
            if (ServiceProvider != null)
            {
                return;
            }

            var services = new ServiceCollection();
            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Sample"
            }.Initialize();

            IndexCreation.CreateIndexes(typeof(CompanyRepository).Assembly, store);

            services.AddSingleton<IDocumentStore>(store);
            services.AddScoped<IDbStore, RavenDbStore>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            ServiceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>()
        {
            using var scope = ServiceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}
