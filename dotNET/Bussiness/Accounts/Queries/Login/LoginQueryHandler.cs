using Bussiness.Accounts.Dtos;
using Bussiness.Accounts.Utils.Token;
using Core.DataAccess;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;
using System.Security.Claims;

namespace Bussiness.Accounts.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginInfo>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenHelper _tokenHelper;

        private readonly IEntityRepository<Account> _accountRepository;
        private readonly IEntityRepository<User> _userRepository;

        public LoginQueryHandler(IUnitOfWork unitOfWork, TokenHelper tokenHelper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;

            _accountRepository = _unitOfWork.GetRepository<Account>();
            _userRepository = _unitOfWork.GetRepository<User>();
        }

        public async Task<Result<LoginInfo>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var accountToCheck = await _accountRepository.Get(acc => acc.Email == request.Email);

            if (!HashingHelper.VerifyPasswordHash(request.Password, accountToCheck.PasswordHash, accountToCheck.PasswordSalt))
            {
                throw new Exception("Wrong password!");
            }

            var user = await _userRepository.GetById(accountToCheck.UserId);

            List<Claim> claims = new()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            _tokenHelper.GenerateAccessToken(claims);
            var refreshToken = _tokenHelper.GenerateRefreshToken();

            accountToCheck.RefreshToken = refreshToken;
            accountToCheck.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.SaveAsync();

            LoginInfo response = new()
            {
                UserName = $"{user.FirstName} {user.LastName}"
            };

            return Result<LoginInfo>.Success(response);
        }
    }
}
