using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SinglePageSample.WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private IEmployeeRepository EmployeeRepository;
        private ICompanyRepository CompanyRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        {
            this.EmployeeRepository = employeeRepository;
            this.CompanyRepository = companyRepository;
        }

        [AcceptVerbs("GET")]
        public int GetTotalEmployees(string name, int? companyId = null)
        {
            return this.EmployeeRepository.TotalEmployeeCriteriaByName(name, companyId);
        }

        [AcceptVerbs("GET")]
        public IEnumerable<Employee> GetPagingSearchEmployees(int currentPage, string name, int? companyId = null)
        {
            return this.EmployeeRepository.PagingEmployeesCriteriaByName(currentPage, name, companyId);
        }

        public HttpResponseMessage PostEmployee(Employee employee)
        {
            var company = this.CompanyRepository.GetById(employee.CompanyId);
            if (company == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            // update company name
            employee.CompanyName = company.Name;
            // should the result status return from repository
            this.EmployeeRepository.Insert(employee);
            if (employee.Id > 0)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}