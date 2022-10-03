using Bussiness.Accounts.Utils.Token;
using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bussiness.Accounts.Commands.GetNewToken
{
    public class GetNewTokenCommandHandler : IRequestHandler<GetNewTokenCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenHelper _tokenHelper;

        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Account> _accountRepository;

        public GetNewTokenCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, TokenHelper tokenHelper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _httpContextAccessor = contextAccessor;

            _userRepository = _unitOfWork.GetRepository<User>();
            _accountRepository = _unitOfWork.GetRepository<Account>();
        }

        public async Task<Result> Handle(GetNewTokenCommand request, CancellationToken cancellationToken)
        {
            string accessToken = _httpContextAccessor.HttpContext.Request.Cookies["accessToken"];
            string refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

            var principal = _tokenHelper.GetPrincipalFromExpiredToken(accessToken);

            var claim = principal.FindFirst("UserId") ?? throw new Exception("Invalid client request");
            var userId = Convert.ToInt64(claim.Value);

            var account = await _accountRepository.Get(acc => acc.UserId == userId);

            if (account is null)
            {
                throw new Exception("Invalid client request");
            }

            if (account.RefreshToken != refreshToken)
            {
                if (account.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    account.RefreshToken = null;
                    account.RefreshTokenExpiryTime = null;
                    await _unitOfWork.SaveAsync();
                }

                throw new Exception("Invalid client request");
            }

            var newAccessToken = _tokenHelper.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenHelper.GenerateRefreshToken();

            account.RefreshToken = newRefreshToken;

            await _unitOfWork.SaveAsync();

            return Result.Success();
        }
    }
}
