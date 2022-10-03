using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Address> _addressRepository;

        public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _addressRepository = _unitOfWork.GetRepository<Address>();
        }

        public async Task<Result> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {             
            var newAddress = new Address
            {
                Name = request.Name,
                Country = request.Country,
                City = request.City,
                State = request.State,
                Street = request.Street,
                PostCode = request.PostCode,
                UserId = _contextAccessor.UserId
            };

            await _addressRepository.Add(newAddress);

            await _unitOfWork.SaveAsync();

            return Result.Success("Address successfully added.");
        }
    }
}
