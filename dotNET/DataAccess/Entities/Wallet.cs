using Core.Entities;

namespace DataAccess.Entities
{
    public class Wallet : BaseEntity
    {
        public long UserId { get; set; }
        public int Balance { get; set; }
    }
}
