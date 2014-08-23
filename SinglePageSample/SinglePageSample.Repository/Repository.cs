using SinglePageSample.Db;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Linq;
using System.Linq.Expressions;
using Raven.Client;
using SinglePageSample.Repository.Expressions;

namespace SinglePageSample.Repository
{
    public class Repository<T> : IRepository<T>
        where T: IEntity
    {
        protected readonly int PAGE_SIZE = 12;

        protected IDbStore DbStore;

        public Repository(IDbStore dbStore)
        {
            this.DbStore = dbStore;
        }

        public void Insert(T entity)
        {
            DbStore.Save(entity);
        }

        public void Delete(T entity)
        {
            DbStore.Delete(entity);
        }

        public T GetById(int id)
        {
            return DbStore.Load<T>(id.ToString());
        }

        public IQueryable<T> Query(string indexName)
        {
            return DbStore.Query<T>(indexName);
        }

        public int Count()
        {
            return DbStore.QuickCount<T>();
        }

        private IEnumerable<T> SearchByWildCard<T>(string indexName, string searchTerms, Expression<Func<T, object>> fieldSelector)
        {
            return DbStore.Search<T>(indexName, fieldSelector, searchTerms, EscapeQueryOptions.AllowAllWildcards);
        }

        protected IEnumerable<T> SingleSearch(string indexName, string searchTerms, IExpression<T> exp)
        {
            return this.SearchByWildCard<T>(indexName, searchTerms, exp.Expression());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
