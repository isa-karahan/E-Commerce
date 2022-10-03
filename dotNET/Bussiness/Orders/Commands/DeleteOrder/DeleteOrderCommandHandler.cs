using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<OrderDetail> _orderDetailRepository;
        private readonly IEntityRepository<OrderItem> _orderItemRepository;
        private readonly IEntityRepository<Transaction> _transactionRepository;
        private readonly IEntityRepository<Wallet> _walletRepository;
        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Category> _categoryRepository;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            _transactionRepository = _unitOfWork.GetRepository<Transaction>();
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
            _productRepository = _unitOfWork.GetRepository<Product>();
            _categoryRepository = _unitOfWork.GetRepository<Category>();
        }
        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            // Get and delete order details
            var orderDetailtoDelete = await _orderDetailRepository.GetById(request.OrderId);

            await _orderDetailRepository.Delete(orderDetailtoDelete);

            // get order items
            var orderItems = await _orderItemRepository.GetAll(orderItem => orderItem.OrderDetailId == request.OrderId);

            await RemoveBonuses(orderItems, orderDetailtoDelete.UserId);

            await AddTransaction(orderDetailtoDelete.UserId, orderDetailtoDelete.TotalPrice, "Refund");

            return Result.Success("Order successfully canceled.");
        }

        private async Task RemoveBonuses(List<OrderItem> orderItems, long userId)
        {
            // delete bonuses from the wallet
            double bonusAmount = 0;

            orderItems.ForEach(item =>
            {
                var product = _productRepository.GetById(item.ProductId).Result;
                var category = _categoryRepository.GetById(product.CategoryId).Result;
                bonusAmount -= item.Quantity * product.UnitPrice * (category.BonusPercentage / 100.0);

                _orderItemRepository.Delete(item);
            });

            await AddTransaction(userId, (int)bonusAmount, "Cashback Return");
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
