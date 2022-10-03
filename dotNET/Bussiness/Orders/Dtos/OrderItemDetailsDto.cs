namespace Bussiness.Orders.Dtos
{
    public class OrderItemDetailsDto
    {
        public int Quantity { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int UnitPrice { get; set; }
    }
}
