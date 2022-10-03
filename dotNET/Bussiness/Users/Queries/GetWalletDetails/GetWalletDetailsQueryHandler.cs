using Bussiness.Users.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Queries.GetWalletDetails
{
    public class GetWalletDetailsQueryHandler : IRequestHandler<GetWalletDetailsQuery, Result<WalletDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Wallet> _walletRepository;
        private readonly IEntityRepository<Transaction> _transactionRepository;
        private readonly IEntityRepository<User> _userRepository;

        public GetWalletDetailsQueryHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _userRepository = _unitOfWork.GetRepository<User>();
            _transactionRepository = _unitOfWork.GetRepository<Transaction>();
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
        }

        public async Task<Result<WalletDetailsDto>> Handle(GetWalletDetailsQuery request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.Get(w => w.UserId == _contextAccessor.UserId);

            var transactions = await _transactionRepository.GetAll(t => t.WalletId == wallet.Id);

            return Result<WalletDetailsDto>.Success(new()
            {
                Balance = wallet.Balance,
                WalletId = wallet.Id,
                Transactions = transactions
            });
        }
    }
}
