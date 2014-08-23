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
    public class CompanyController : ApiController
    {
        private ICompanyRepository CompanyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.CompanyRepository = companyRepository;
        }

        [AcceptVerbs("GET")]
        public IEnumerable<Company> GetPagingCompanies(int currentPage)
        {
            return this.CompanyRepository.GetPagingCompanies(currentPage);
        }

        [AcceptVerbs("GET")]
        public int GetTotalCompanies()
        {
            return this.CompanyRepository.Count();
        }

        public HttpResponseMessage PostCompany(Company company)
        {
            this.CompanyRepository.Insert(company);
            if (company.Id > 0)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }
    }
}