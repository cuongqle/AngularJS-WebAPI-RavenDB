using SinglePageSample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository.Interfaces
{
    public interface ICompanyRepository: IRepository<Company>
    {
        IEnumerable<Company> GetPagingCompanies(int currentPage);
    }
}
