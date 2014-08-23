using Raven.Client.Indexes;
using SinglePageSample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
