using DataAccess.Entities;

namespace Bussiness.Users.Dtos
{
    public class WalletDetailsDto
    {
        public long WalletId { get; set; }
        public int Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
