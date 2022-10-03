using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Commands.DepositMoney
{
    public class DepositMoneyCommandHandler : IRequestHandler<DepositMoneyCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Wallet> _walletRepository;
        private readonly IEntityRepository<Transaction> _transactionRepository;

        public DepositMoneyCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _transactionRepository = _unitOfWork.GetRepository<Transaction>();
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
        }
        public async Task<Result> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.Get(w => w.UserId == _contextAccessor.UserId);

            wallet.Balance += request.Amount;

            await _transactionRepository.Add(new()
            {
                Amount = request.Amount,
                WalletId = wallet.Id
            });

            await _unitOfWork.SaveAsync();

            return Result.Success("Deposit is successfull!");
        }
    }
}
