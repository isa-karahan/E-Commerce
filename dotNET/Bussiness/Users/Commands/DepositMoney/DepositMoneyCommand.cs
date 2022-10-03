using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Commands.DepositMoney
{
    public class DepositMoneyCommand : ICommand<Result>
    {
        public int Amount { get; set; }
    }
}
