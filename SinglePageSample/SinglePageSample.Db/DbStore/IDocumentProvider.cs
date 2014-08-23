using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Db.DbStore
{
    public interface IDocumentProvider<T>
    {
        T CreateInstance();

        T CreateInstance(string connectionStringName);
    }
}
