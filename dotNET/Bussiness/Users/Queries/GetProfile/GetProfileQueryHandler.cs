using Bussiness.Users.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Queries.GetProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<ProfileDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Account> _accountRepository;
        private readonly IEntityRepository<User> _userRepository;

        public GetProfileQueryHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _userRepository = _unitOfWork.GetRepository<User>();
            _accountRepository = _unitOfWork.GetRepository<Account>();
        }

        public async Task<Result<ProfileDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.UserId;

            var user = await _userRepository.GetById(userId);

            var account = await _accountRepository.Get(acc => acc.UserId == userId);

            return Result<ProfileDto>.Success(new()
            {
                Email = account.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Added = user.Added.ToString("G")
            });
        }
    }
}
