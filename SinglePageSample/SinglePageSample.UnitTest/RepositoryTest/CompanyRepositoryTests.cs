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
    public class CompanyRepositoryTests
    {
        public CompanyRepositoryTests()
        {
            HotSpot.WireUp();
        }

        [TestMethod]
        public void AddBulkCompanies()
        {
            var companyRepository = HotSpot.Resolve<ICompanyRepository>();

            for (int i = 1; i <= 200; i++)
            {
                var company = new Company()
                {
                    Id = i,
                    Name = "Company " + i,
                    Description = "This is company " + i
                };

                companyRepository.Insert(company);
            }
        }

        [TestMethod]
        public void TheCompany_200_ShouldBeNotNull()
        {
            var companyRepository = HotSpot.Resolve<ICompanyRepository>();
            var company = companyRepository.GetById(200);
            Assert.IsNotNull(company);
        }

        [TestMethod]
        public void TotalCompaniesShouldBe_200()
        {
            var companyRepository = HotSpot.Resolve<ICompanyRepository>();
            var total = companyRepository.Count();
            Assert.AreEqual(total, 200);
        }
    }
}
