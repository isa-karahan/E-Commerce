using Bussiness.Accounts.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Middlewares.Exceptions;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Accounts.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Account> _accountRepository;

        public UpdatePasswordCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _accountRepository = _unitOfWork.GetRepository<Account>();
        }

        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var accountToUpdate = await _accountRepository.Get(acc => acc.UserId == _contextAccessor.UserId);

            if (accountToUpdate == null)
            {
                throw new NotFoundException("Account not found!");
            }


            if (!HashingHelper.VerifyPasswordHash(request.CurrentPassword, accountToUpdate.PasswordHash, accountToUpdate.PasswordSalt))
            {
                throw new Exception("Wrong Password!");
            }

            HashingHelper.CreatePasswordHash(request.NewPassword, out byte[] newHash, out byte[] newSalt);

            accountToUpdate.PasswordHash = newHash;
            accountToUpdate.PasswordSalt = newSalt;

            await _accountRepository.Update(accountToUpdate);
            await _unitOfWork.SaveAsync();

            return Result.Success("Account successfully updated");
        }
    }
}
