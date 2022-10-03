using Core.Entities;

namespace DataAccess.Entities
{
    public class OrderItem : BaseEntity
    {
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
