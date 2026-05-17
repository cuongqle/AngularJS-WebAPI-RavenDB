using Microsoft.AspNetCore.Mvc;
using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace SinglePageSample.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository CompanyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.CompanyRepository = companyRepository;
        }

        [HttpGet]
        public IEnumerable<Company> GetPagingCompanies(int currentPage)
        {
            return this.CompanyRepository.GetPagingCompanies(currentPage);
        }

        [HttpGet]
        public int GetTotalCompanies()
        {
            return this.CompanyRepository.Count();
        }

        [HttpPost]
        public IActionResult PostCompany([FromBody] Company company)
        {
            this.CompanyRepository.Insert(company);
            if (company.Id > 0)
            {
                return Ok();
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}