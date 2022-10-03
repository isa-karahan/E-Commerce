namespace Bussiness.Orders.Dtos
{
    public class OrderDetailsDto
    {
        public long OrderDetailId { get; set; }
        public string OrderAddress { get; set; } = string.Empty;
        public List<OrderItemDetailsDto> OrderItems { get; set; } = new List<OrderItemDetailsDto>();
        public int TotalPrice { get; set; }
        public string OrderDate { get; set; }
    }
}
