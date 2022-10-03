using Bussiness.Orders.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IQuery<Result<List<OrderDetailsDto>>>
    { }
}
