using SinglePageSample.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository.Expressions
{
    public class EmployeeByNameExpression : IExpression<Employee>
    {
        public Expression<Func<Employee, object>> Expression()
        {
            return e => e.Name;
        }
    }
}
