using Raven.Client.Documents.Indexes;
using SinglePageSample.Repository.Entities;
using System.Linq;

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
