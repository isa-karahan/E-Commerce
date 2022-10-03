using Bussiness.Orders.Commands.DeleteOrder;
using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Users.Commands.DeleteAddress
{
    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<Address> _addressRepository;

        public DeleteAddressCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _addressRepository = _unitOfWork.GetRepository<Address>();

            RuleFor(a => a.AddressId).NotEmpty()
                .MustAsync(async (id, ct) => await CheckIfAddressExists(id))
                .WithMessage("Address not found.");
        }

        private async Task<bool> CheckIfAddressExists(long addressId)
        {
            var address = await _addressRepository.GetById(addressId);

            return address != null;
        }
    }
}
