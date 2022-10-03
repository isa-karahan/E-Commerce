using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Accounts.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<Account> _accountRepository;
        public LoginQueryValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepository = _unitOfWork.GetRepository<Account>();

            RuleFor(l => l.Email).NotEmpty().WithMessage("Email is required.")
                                          .EmailAddress().WithMessage("Invalid email format.")
                                          .MustAsync(async (email, canc) => await CheckIfAccountExists(email))
                                          .WithMessage("Account not found.");

            RuleFor(l => l.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }

        private async Task<bool> CheckIfAccountExists(string email)
        {
            var account = await _accountRepository.Get(a => a.Email == email);

            return account != null;
        }
    }
}
