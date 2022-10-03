using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<Address> _addressRepository;

        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _addressRepository = _unitOfWork.GetRepository<Address>();
        }
        public async Task<Result> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var addressToDelete = await _addressRepository.GetById(request.AddressId);
            await _addressRepository.Delete(addressToDelete);

            await _unitOfWork.SaveAsync();

            return Result.Success("Address successfully deleted.");
        }
    }
}
