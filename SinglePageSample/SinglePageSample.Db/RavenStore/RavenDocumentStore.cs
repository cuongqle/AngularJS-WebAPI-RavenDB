using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Db.RavenStore
{
    public class RavenDocumentStore: DocumentStore
    {
        private static readonly string DEFAULT_CONNECTION = "";

        public RavenDocumentStore()
            : this(DEFAULT_CONNECTION)
        {
        }

        public RavenDocumentStore(string connectionStringName)
        {
            this.ConnectionStringName = connectionStringName;
            this.Initialize();
        }
    }
}
