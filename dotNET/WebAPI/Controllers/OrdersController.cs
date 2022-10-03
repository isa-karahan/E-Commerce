using Bussiness.Orders.Commands.CreateOrder;
using Bussiness.Orders.Commands.DeleteOrder;
using Bussiness.Orders.Dtos;
using Bussiness.Orders.Queries.GetOrders;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/Users/[Controller]")]
    public class OrdersController : BaseController
    {
        public OrdersController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        public async Task<Result<List<OrderDetailsDto>>> GetAll()
        {
            return await Mediator.Send(new GetOrdersQuery());
        }

        [HttpPost]
        public async Task<Result> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            return await Mediator.Send(createOrderCommand);
        }

        [HttpDelete]
        public async Task<Result> CancelOrder(DeleteOrderCommand deleteOrderCommand)
        {
            return await Mediator.Send(deleteOrderCommand);
        }
    }
}
