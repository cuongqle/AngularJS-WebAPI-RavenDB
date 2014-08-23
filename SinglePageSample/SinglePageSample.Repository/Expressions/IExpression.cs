using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SinglePageSample.Repository.Expressions
{
    public interface IExpression<T>
    {
        Expression<Func<T, object>> Expression();
    }
}
