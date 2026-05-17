using SinglePageSample.Db.DbStore;
using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Indexes;
using SinglePageSample.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SinglePageSample.Repository
{
    public class CompanyRepository: Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbStore dbStore)
            : base(dbStore)
        {
        }

        public IEnumerable<Company> GetPagingCompanies(int currentPage)
        {
            var query = this.DbStore.Query<Company>(typeof(CompanyIndex).Name);
            return query.Skip((currentPage - 1) * PAGE_SIZE).Take(PAGE_SIZE);
        }
    }
}
