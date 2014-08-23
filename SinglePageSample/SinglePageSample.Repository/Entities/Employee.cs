using SinglePageSample.Db.DbStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinglePageSample.Repository.Entities
{
    public class Employee: IEntity
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}