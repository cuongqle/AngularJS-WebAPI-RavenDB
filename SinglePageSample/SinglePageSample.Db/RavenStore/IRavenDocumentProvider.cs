using Raven.Client;
using SinglePageSample.Db.DbStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Db.RavenStore
{
    public interface IRavenDocumentProvider : IDocumentProvider<IDocumentStore>
    {
    }
}
