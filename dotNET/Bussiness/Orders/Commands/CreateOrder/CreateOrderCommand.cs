using Bussiness.Orders.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : ICommand<Result>
    {
        public long AddressId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
