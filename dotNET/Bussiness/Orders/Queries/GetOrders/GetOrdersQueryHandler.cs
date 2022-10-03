using Bussiness.Orders.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Middlewares.Exceptions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Result<List<OrderDetailsDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<OrderDetail> _orderDetailRepository;
        private readonly IEntityRepository<OrderItem> _orderItemRepository;
        private readonly IEntityRepository<Address> _addressRepository;
        private readonly IEntityRepository<Product> _productRepository;

        public GetOrdersQueryHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _addressRepository = _unitOfWork.GetRepository<Address>();
            _productRepository = _unitOfWork.GetRepository<Product>();
            _orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
        }

        public async Task<Result<List<OrderDetailsDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orderDetails = await _orderDetailRepository.GetAll(od => od.UserId == _contextAccessor.UserId);

            var items = await _orderItemRepository.GetAll();

            var result = new List<OrderDetailsDto>();

            foreach (OrderDetail orderDetail in orderDetails)
            {
                var orderItemDetails = new List<OrderItemDetailsDto>();

                foreach (OrderItem item in items)
                {
                    if (item.OrderDetailId == orderDetail.Id)
                    {
                        var product = await _productRepository.GetById(item.ProductId) ?? throw new NotFoundException("Product not found!");

                        orderItemDetails.Add(new()
                        {
                            ProductName = product.Name,
                            Quantity = item.Quantity,
                            UnitPrice = product.UnitPrice,
                            ProductId = item.ProductId
                        });
                    }
                }

                var address = await _addressRepository.GetById(orderDetail.AddressId);

                result.Add(new()
                {
                    OrderAddress = address.ToString(),
                    OrderDate = orderDetail.OrderDate.ToString("G"),
                    OrderDetailId = orderDetail.Id,
                    TotalPrice = orderDetail.TotalPrice,
                    OrderItems = orderItemDetails
                });
            }

            return Result<List<OrderDetailsDto>>.Success(result);
        }
    }
}
