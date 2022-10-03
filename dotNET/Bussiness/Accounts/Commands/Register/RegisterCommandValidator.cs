using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Accounts.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<Account> _accountRepository;
        public RegisterCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = _unitOfWork.GetRepository<Account>();

            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required.")
                                          .EmailAddress().WithMessage("Invalid email format.")
                                          .MustAsync(async (email, canc) => await CheckIfAccountExists(email))
                                          .WithMessage("Account exists with this email.");

            RuleFor(r => r.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                                       .MinimumLength(10).MaximumLength(15);

            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required.");
        }

        private async Task<bool> CheckIfAccountExists(string email)
        {
            var account = await _accountRepository.Get(a => a.Email == email);

            return account is null;
        }
    }
}
