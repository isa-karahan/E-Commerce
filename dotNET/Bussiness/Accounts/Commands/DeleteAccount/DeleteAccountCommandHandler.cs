using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Account> _accountRepository;
        private readonly IEntityRepository<Address> _addressRepository;
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Wallet> _walletRepository;
        private readonly IEntityRepository<OrderDetail> _orderDetailRepository;
        private readonly IEntityRepository<OrderItem> _orderItemRepository;
        private readonly IEntityRepository<Transaction> _transactionRepository;
        private readonly IEntityRepository<Favorite> _favoriteRepository;
        private readonly IEntityRepository<Comment> _commentRepository;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _accountRepository = _unitOfWork.GetRepository<Account>();
            _addressRepository = _unitOfWork.GetRepository<Address>();
            _userRepository = _unitOfWork.GetRepository<User>();
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
            _orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();
            _orderItemRepository = _unitOfWork.GetRepository<OrderItem>();
            _transactionRepository = _unitOfWork.GetRepository<Transaction>();
            _favoriteRepository = _unitOfWork.GetRepository<Favorite>();
            _commentRepository = _unitOfWork.GetRepository<Comment>();
        }

        public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.UserId;

            var account = await _accountRepository.Get(a => a.UserId == userId);
            await _accountRepository.Delete(account);

            var addresses = await _addressRepository.GetAll(a => a.UserId == userId);
            foreach (var address in addresses)
                await _addressRepository.Delete(address);

            var user = await _userRepository.GetById(userId);
            await _userRepository.Delete(user);

            var wallet = await _walletRepository.Get(w => w.UserId == userId);
            await _walletRepository.Delete(wallet);

            var transactions = await _transactionRepository.GetAll(t => t.WalletId == wallet.Id);
            foreach (var transaction in transactions)
                await _transactionRepository.Delete(transaction);

            var orderDetail = await _orderDetailRepository.GetAll(od => od.UserId == userId);
            foreach (var order in orderDetail)
            {
                var orderItems = await _orderItemRepository.GetAll(item => item.OrderDetailId == order.Id);

                foreach (var orderItem in orderItems)
                    await _orderItemRepository.Delete(orderItem);

                await _orderDetailRepository.Delete(order);
            }

            var favorites = await _favoriteRepository.GetAll(f => f.UserId == userId);
            foreach (var favorite in favorites)
                await _favoriteRepository.Delete(favorite);

            var comments = await _commentRepository.GetAll(c => c.UserId == userId);
            foreach (var comment in comments)
                await _commentRepository.Delete(comment);

            await _unitOfWork.SaveAsync();

            return Result.Success("Account deleted successfully.");
        }
    }
}
