using SinglePageSample.Db.DbStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Indexes;
using System.Globalization;
using System.Data.Entity.Design.PluralizationServices;
using System.Linq.Expressions;

namespace SinglePageSample.Db.RavenStore
{
    public class RavenDbStore: IDbStore
    {
        protected IDocumentStore DocumentStore { get; set; }
        protected IDocumentProvider<IDocumentStore> DocumentProvider { get; set; }

        private string GetFullRavenEntityId<T>(string id)
        {
            string entityName = typeof(T).Name;
            var pluralService = PluralizationService.CreateService(System.Globalization.CultureInfo.CurrentCulture);
            if (pluralService.IsPlural(entityName) == false)
            {
                entityName = pluralService.Pluralize(entityName);
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", entityName.ToLowerInvariant(), id);
        }

        public RavenDbStore(IDocumentProvider<IDocumentStore> documentProvider)
        {
            this.DocumentProvider = documentProvider;
        }

        public RavenDbStore(IDocumentProvider<IDocumentStore> documentProvider, string connectionName) :
            this(documentProvider)
        {
            this.DocumentStore = this.DocumentProvider.CreateInstance(connectionName);
        }

        public T Load<T>(string id)
        {
            using (var session = this.DocumentStore.OpenSession())
            {
                return session.Load<T>(this.GetFullRavenEntityId<T>(id));
            }
        }

        public T Load<T>(Guid id)
        {
            using (var session = this.DocumentStore.OpenSession())
            {
                return session.Load<T>(this.GetFullRavenEntityId<T>(id.ToString()));
            }
        }

        public IQueryable<T> Query<T>(string indexName)
        {
            using (var session = this.DocumentStore.OpenSession())
            {
                return session.Query<T>(indexName);
            }
        }

        public IQueryable<T> Search<T>(string indexName, Expression<Func<T, object>> fieldSelector, 
            string searchTerms, EscapeQueryOptions options)
        {
            using (var session = this.DocumentStore.OpenSession())
            {
                return session.Query<T>(indexName).Search<T>(fieldSelector, searchTerms, escapeQueryOptions: options);
            }
        }

        public void Save<T>(T entity)
        {
            if (entity != null)
            {
                using (var session = this.DocumentStore.OpenSession())
                {
                    session.Advanced.UseOptimisticConcurrency = false;
                    session.Store(entity);
                    session.SaveChanges();
                }
            }
            else
            {
                throw new ArgumentException("entity");
            }
        }

        public void Save<T>(T entity, string id)
        {
            if (entity != null)
            {
                using (var session = this.DocumentStore.OpenSession())
                {
                    session.Advanced.UseOptimisticConcurrency = false;
                    session.Store(entity, id);
                    session.SaveChanges();
                }
            }
            else
            {
                throw new ArgumentException("entity");
            }
        }

        public void Save<T>(T entity, Guid id)
        {
            this.Save<T>(entity, id.ToString());
        }

        public void Delete<T>(T entity)
        {
            if (entity != null)
            {
                using (var session = this.DocumentStore.OpenSession())
                {
                    session.Advanced.UseOptimisticConcurrency = false;
                    session.Delete(entity);
                    session.SaveChanges();
                }
            }
            else
            {
                throw new ArgumentException("entity");
            }
        }

        public int QuickCount<T>() 
        {
            using (var session = this.DocumentStore.OpenSession())
            {
                RavenQueryStatistics statistics;
                session.Query<T>().Statistics(out statistics).Take(0).ToArray();
                return statistics.TotalResults;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
