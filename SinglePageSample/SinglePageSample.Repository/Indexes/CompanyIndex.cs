using Raven.Client.Indexes;
using SinglePageSample.Db.RavenStore;
using SinglePageSample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository.Indexes
{
    public class CompanyIndex : AbstractIndexCreationTask<Company>
    {
        public CompanyIndex()
        {
            this.Map = companies => from company in companies
                                    select new
                                    {
                                        company.Id,
                                        company.Name,
                                        company.Description
                                    };            
        }
    }
}
