using Core.Entities;

namespace DataAccess.Entities
{
    public class Product : BaseEntity
    {
        public long CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
