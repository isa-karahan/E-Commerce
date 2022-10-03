using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Users.Commands.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<Address> _addressRepository;

        public CreateAddressCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _addressRepository = _unitOfWork.GetRepository<Address>();

            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is required.")
                .MustAsync(async (name, ct) => await CheckIfAddressNameExists(name))
                .WithMessage("Address name must be unique.");

            RuleFor(a => a.Country).NotEmpty().WithMessage("Country is required.");
            RuleFor(a => a.City).NotEmpty().WithMessage("City is required.");
            RuleFor(a => a.State).NotEmpty().WithMessage("State is required.");
            RuleFor(a => a.Street).NotEmpty().WithMessage("Street is required.");
            RuleFor(a => a.PostCode).NotEmpty().WithMessage("Post code is required.").MinimumLength(5);
        }

        private async Task<bool> CheckIfAddressNameExists(string name)
        {
            var address = await _addressRepository.Get(a => a.Name == name);

            return address != null;
        }
    }
}
