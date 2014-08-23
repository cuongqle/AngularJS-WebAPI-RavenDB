using SinglePageSample.Db.DbStore;
using SinglePageSample.Repository.Entities;
using SinglePageSample.Repository.Expressions;
using SinglePageSample.Repository.Indexes;
using SinglePageSample.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository
{
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbStore dbStore)
            : base(dbStore)
        {
        }
        
        public int TotalEmployeeCriteriaByName(string name, int? companyId)
        {
            string searchTerms = string.Format("{0}{1}{0}", "*", name);

            var query = this.SingleSearch(typeof(EmployeeIndex).Name, searchTerms, new EmployeeByNameExpression());

            // by company
            if (companyId.HasValue)
            {
                query = query.Where(c => c.CompanyId == companyId);
            }

            return query.Count();
        }

        public IEnumerable<Employee> PagingEmployeesCriteriaByName(int currentPage, string name, int? companyId)
        {
            string searchTerms = string.Format("{0}{1}{0}", "*", name);

            var query = this.SingleSearch(typeof(EmployeeIndex).Name, searchTerms, new EmployeeByNameExpression());

            // by company
            if (companyId.HasValue)
            {
                query = query.Where(c => c.CompanyId == companyId);
            }

            return query.Skip((currentPage - 1) * PAGE_SIZE).Take(PAGE_SIZE);
        }
    }
}