using Azure;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Accounts.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<Account> _accountRepository;

        private readonly IUserContextAccessor _contextAccessor;

        public LogoutCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _accountRepository = _unitOfWork.GetRepository<Account>();
        }

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.Get(acc => acc.UserId == _contextAccessor.UserId);

            account.RefreshToken = null;
            account.RefreshTokenExpiryTime = null;

            await _unitOfWork.SaveAsync();

            return Result.Success("Logged out successfully.");
        }
    }
}
