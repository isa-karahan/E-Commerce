using Core.Entities;

namespace DataAccess.Entities
{
    public class Favorite : BaseEntity
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
    }
}
