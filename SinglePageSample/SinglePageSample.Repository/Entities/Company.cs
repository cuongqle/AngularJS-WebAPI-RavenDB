using SinglePageSample.Db.DbStore;

namespace SinglePageSample.Repository.Entities
{
    public class Company : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}