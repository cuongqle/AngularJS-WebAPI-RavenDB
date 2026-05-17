using Microsoft.AspNetCore.Mvc;
using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace SinglePageSample.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository EmployeeRepository;
        private readonly ICompanyRepository CompanyRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        {
            this.EmployeeRepository = employeeRepository;
            this.CompanyRepository = companyRepository;
        }

        [HttpGet]
        public int GetTotalEmployees(string name, int? companyId = null)
        {
            return this.EmployeeRepository.TotalEmployeeCriteriaByName(name, companyId);
        }

        [HttpGet]
        public IEnumerable<Employee> GetPagingSearchEmployees(int currentPage, string name, int? companyId = null)
        {
            return this.EmployeeRepository.PagingEmployeesCriteriaByName(currentPage, name, companyId);
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] Employee employee)
        {
            var company = this.CompanyRepository.GetById(employee.CompanyId);
            if (company == null)
            {
                return NotFound();
            }

            // update company name
            employee.CompanyName = company.Name;
            // should the result status return from repository
            this.EmployeeRepository.Insert(employee);
            if (employee.Id > 0)
            {
                return Ok();
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}