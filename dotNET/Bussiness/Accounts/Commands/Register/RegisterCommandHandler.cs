using Core.DataAccess;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Accounts.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<Account> _accountRepository;
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Wallet> _walletRepository;

        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _accountRepository = _unitOfWork.GetRepository<Account>();
            _userRepository = _unitOfWork.GetRepository<User>();
            _walletRepository = _unitOfWork.GetRepository<Wallet>();
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.Get(acc => acc.Email == request.Email);

            User newUser = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };

            await _userRepository.Add(newUser);
            await _unitOfWork.SaveAsync();

            HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            Account newAccount = new()
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UserId = newUser.Id
            };

            await _accountRepository.Add(newAccount);

            await _walletRepository.Add(new()
            {
                UserId = newUser.Id,
                Balance = 0
            });

            await _unitOfWork.SaveAsync();

            return Result.Success("Registration successfull.");
        }
    }
}
