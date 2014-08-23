using SinglePageSample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository.Interfaces
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        IEnumerable<Employee> PagingEmployeesCriteriaByName(int currentPage, string name, int? companyId);

        int TotalEmployeeCriteriaByName(string name, int? companyId);
    }
}
