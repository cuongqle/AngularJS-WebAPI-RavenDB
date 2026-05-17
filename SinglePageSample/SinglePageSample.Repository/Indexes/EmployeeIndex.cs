using Raven.Client.Documents.Indexes;
using SinglePageSample.Repository.Entities;
using System.Linq;

namespace SinglePageSample.Repository.Indexes
{
    public class EmployeeIndex : AbstractIndexCreationTask<Employee>
    {
        public EmployeeIndex()
        {
            this.Map = employees => from employee in employees
                                    select new
                                    {
                                        employee.Id,
                                        employee.CompanyId,
                                        employee.CompanyName,
                                        employee.Name,
                                        employee.Description
                                    };            
        }
    }
}
