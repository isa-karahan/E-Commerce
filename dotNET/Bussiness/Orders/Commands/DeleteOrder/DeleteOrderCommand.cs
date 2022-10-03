using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : ICommand<Result>
    {
        public long OrderId { get; set; }
    }
}
