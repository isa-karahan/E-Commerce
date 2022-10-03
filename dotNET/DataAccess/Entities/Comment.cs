using Core.Entities;

namespace DataAccess.Entities
{
    public class Comment : BaseEntity
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Added { get; set; } = DateTime.Now;
    }
}
