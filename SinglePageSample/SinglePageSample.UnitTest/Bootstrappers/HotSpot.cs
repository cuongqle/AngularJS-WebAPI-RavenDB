using Microsoft.Extensions.DependencyInjection;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Repository;
using SinglePageSample.Repository.Interfaces;
using SinglePageSample.UnitTest.InMemoryStore;
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

            services.AddSingleton<IDbStore, InMemoryDbStore>();
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
