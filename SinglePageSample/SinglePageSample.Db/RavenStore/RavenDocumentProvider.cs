using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Db.RavenStore
{
    public class RavenDocumentProvider : IRavenDocumentProvider
    {
        public IDocumentStore CreateInstance()
        {
            return new RavenDocumentStore();
        }

        public IDocumentStore CreateInstance(string connectionStringName)
        {
            return new RavenDocumentStore(connectionStringName);
        }
    }
}
