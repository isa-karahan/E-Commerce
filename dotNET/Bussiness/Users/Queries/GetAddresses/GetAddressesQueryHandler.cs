using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Queries.GetAddresses
{
    public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, Result<List<Address>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Address> _addressRepository;

        public GetAddressesQueryHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _addressRepository = _unitOfWork.GetRepository<Address>();
        }

        public async Task<Result<List<Address>>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.GetAll(a => a.UserId == _contextAccessor.UserId);

            return Result<List<Address>>.Success(addresses);
        }
    }
}
