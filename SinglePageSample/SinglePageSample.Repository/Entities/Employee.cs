using SinglePageSample.Db.DbStore;

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