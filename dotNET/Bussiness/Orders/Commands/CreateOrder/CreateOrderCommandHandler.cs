using Bussiness.Orders.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<OrderDetail> _orderDetailRepository;
        private readonly IEntityRepository<OrderItem> _orderItemRepository;
        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IEntityRepository<Transaction> _transactionRepository;
        private readonly IEntityRepository<Wallet> _walletRepository;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            _transactionRepository = _unitOfWork.GetRepository<Transaction>();
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
            _productRepository = _unitOfWork.GetRepository<Product>();
            _categoryRepository = _unitOfWork.GetRepository<Category>();
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.UserId;

            // Calculate total price
            var totalPrice = await CalculateTotalPrice(request.OrderItems);

            await AddTransaction(userId, -totalPrice, "Payment");

            // First, add new order detail to get orderDetailId
            var newOrderDetail = await _orderDetailRepository.Add(new OrderDetail
            {
                AddressId = request.AddressId,
                UserId = userId,
                TotalPrice = totalPrice
            });

            await _unitOfWork.SaveAsync();

            // Then add new order items
            foreach (CreateOrderItemDto item in request.OrderItems)
            {
                var orderItemToCreate = new OrderItem
                {
                    OrderDetailId = newOrderDetail.Id,
                    ProductId = item.Product,
                    Quantity = item.Quantity
                };
                await _orderItemRepository.Add(orderItemToCreate);
            }

            await AddBonuses(request.OrderItems, userId);

            await _unitOfWork.SaveAsync();
            return Result.Success("Order successfully created.");
        }

        private async Task<int> CalculateTotalPrice(List<CreateOrderItemDto> orderItems)
        {
            var totalPrice = 0;

            foreach (var item in orderItems)
            {
                var product = await _productRepository.GetById(item.Product);

                totalPrice += product.UnitPrice * item.Quantity;
            }

            return totalPrice;
        }

        private async Task AddBonuses(List<CreateOrderItemDto> orderItems, long userId)
        {
            // add bonuses to the wallet
            double bonusAmount = 0;

            orderItems.ForEach(item =>
            {
                var product = _productRepository.GetById(item.Product).Result;
                product.UnitsInStock -= item.Quantity;

                var category = _categoryRepository.GetById(product.CategoryId).Result;
                bonusAmount += item.Quantity * product.UnitPrice * (category.BonusPercentage / 100.0);
            });

            await _unitOfWork.SaveAsync();

            await AddTransaction(userId, (int)bonusAmount, "Bonus");
        }

        private async Task AddTransaction(long userId, int amount, string type)
        {
            var wallet = await _walletRepository.Get(w => w.UserId == userId);

            await _transactionRepository.Add(new()
            {
                WalletId = wallet.Id,
                Amount = amount,
                Type = type
            });

            wallet.Balance += amount;

            await _walletRepository.Update(wallet);

            await _unitOfWork.SaveAsync();
        }
    }
}
