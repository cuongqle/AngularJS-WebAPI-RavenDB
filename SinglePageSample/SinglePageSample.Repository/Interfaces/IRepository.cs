using SinglePageSample.Db.DbStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T: IEntity
    {
        void Insert(T entity);
        void Delete(T entity);
        IQueryable<T> Query(string indexName);
        int Count();
        T GetById(int id);
    }
}
