using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using SinglePageSample.Db.DbStore;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace SinglePageSample.Db.RavenStore
{
    public class RavenDbStore : IDbStore
    {
        protected IDocumentStore DocumentStore { get; }

        private string GetFullRavenEntityId<T>(string id)
        {
            string entityName = typeof(T).Name;
            entityName = Pluralize(entityName);

            return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", entityName.ToLowerInvariant(), id);
        }

        private static string Pluralize(string entityName)
        {
            if (entityName.EndsWith("y", StringComparison.OrdinalIgnoreCase))
            {
                return entityName[..^1] + "ies";
            }

            if (entityName.EndsWith("s", StringComparison.OrdinalIgnoreCase))
            {
                return entityName;
            }

            return entityName + "s";
        }

        public RavenDbStore(IDocumentStore documentStore)
        {
            DocumentStore = documentStore ?? throw new ArgumentNullException(nameof(documentStore));
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
                return session.Query<T>(indexName).ToList().AsQueryable();
            }
        }

        public IQueryable<T> Search<T>(string indexName, Expression<Func<T, object>> fieldSelector, string searchTerms)
        {
            using (var session = this.DocumentStore.OpenSession())
            {
                return session.Query<T>(indexName).Search(fieldSelector, searchTerms).ToList().AsQueryable();
            }
        }

        public void Save<T>(T entity)
        {
            if (entity != null)
            {
                using (var session = this.DocumentStore.OpenSession())
                {
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
                QueryStatistics statistics;
                session.Query<T>().Statistics(out statistics).Take(0).ToArray();
                return (int)statistics.TotalResults;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
