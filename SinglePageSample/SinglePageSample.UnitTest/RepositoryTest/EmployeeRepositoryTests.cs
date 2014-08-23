using Microsoft.VisualStudio.TestTools.UnitTesting;
using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Interfaces;
using SinglePageSample.UnitTest.Bootstrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.UnitTest.RepositoryTest
{
    [TestClass]
    public class EmployeeRepositoryTests
    {
        public EmployeeRepositoryTests()
        {
            HotSpot.WireUp();
        }

        [TestMethod]
        public void AddBulkEmployees()
        {
            var employeeRepository = HotSpot.Resolve<IEmployeeRepository>();

            for (int i = 1; i <= 200; i++)
            {
                var employee = new Employee()
                {
                    Id = i,
                    CompanyId = i,
                    CompanyName = "This is company " + i,
                    Name = "Employee " + i,
                    Description = "This is employee " + i
                };

                employeeRepository.Insert(employee);
            }
        }
    }
}
