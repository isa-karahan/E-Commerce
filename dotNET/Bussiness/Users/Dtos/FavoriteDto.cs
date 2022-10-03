namespace Bussiness.Users.Dtos
{
    public class FavoriteDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public long ProductId { get; set; }
        public int UnitPrice { get; set; }
        public string Added { get; set; }
    }
}
