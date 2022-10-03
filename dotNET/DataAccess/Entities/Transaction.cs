using Core.Entities;

namespace DataAccess.Entities
{
    public class Transaction : BaseEntity
    {
        public long WalletId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Type { get; set; } = "Deposit";
    }
}
