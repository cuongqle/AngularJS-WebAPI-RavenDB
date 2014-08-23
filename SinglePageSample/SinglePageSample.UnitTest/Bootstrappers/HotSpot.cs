using Microsoft.Practices.Unity;
using Raven.Client;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Db.RavenStore;
using SinglePageSample.Repository;
using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.UnitTest.Bootstrappers
{
    public static class HotSpot
    {
        public static UnityContainer UnityContainer { get; set; }

        public static void WireUp()
        {
            UnityContainer = new UnityContainer();

            UnityContainer.RegisterType<IDocumentProvider<IDocumentStore>, RavenDocumentProvider>(
                new TransientLifetimeManager());
            UnityContainer.RegisterType<IDbStore, RavenDbStore>((
                new TransientLifetimeManager()),
                new InjectionConstructor(new ResolvedParameter<IDocumentProvider<IDocumentStore>>(), "Default"));

            UnityContainer.RegisterType<ICompanyRepository, CompanyRepository>(new TransientLifetimeManager());
            UnityContainer.RegisterType<IEmployeeRepository, EmployeeRepository>(new TransientLifetimeManager());
        }

        public static T Resolve<T>()
        {
            return UnityContainer.Resolve<T>();
        }
    }
}
