using FluentValidation;

namespace Bussiness.Users.Commands.DepositMoney
{
    public class DepositMoneyCommandValidator : AbstractValidator<DepositMoneyCommand>
    {
        public DepositMoneyCommandValidator()
        {
            RuleFor(d => d.Amount).GreaterThan(0)
                .WithMessage("Deposit amount cannot be negative!");
        }
    }
}
