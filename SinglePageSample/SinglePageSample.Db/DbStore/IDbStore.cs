using System;
using System.Linq;
using System.Linq.Expressions;

namespace SinglePageSample.Db.DbStore
{
    public interface IDbStore : IDisposable
    {
        T Load<T>(string id);
        T Load<T>(Guid id);
        void Delete<T>(T entity);
        int QuickCount<T>();
        IQueryable<T> Query<T>(string indexName);
        IQueryable<T> Search<T>(string indexName, Expression<Func<T, object>> fieldSelector, string searchTerms);
        void Save<T>(T entity);
        void Save<T>(T entity, string id);
        void Save<T>(T entity, Guid id);
    }
}
