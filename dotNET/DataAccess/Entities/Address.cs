using Core.Entities;

namespace DataAccess.Entities
{
    public class Address : BaseEntity
    {
        public long UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name}: {Street} {PostCode} - {State}/{City}/{Country}";
        }
    }
}
