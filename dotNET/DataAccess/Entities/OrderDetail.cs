using Core.Entities;

namespace DataAccess.Entities
{
    public class OrderDetail : BaseEntity
    {
        public long UserId { get; set; }
        public long AddressId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
