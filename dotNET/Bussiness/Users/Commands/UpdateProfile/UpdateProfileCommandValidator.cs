using FluentValidation;

namespace Bussiness.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(r => r.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                                       .MinimumLength(10).MaximumLength(15);

            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is required.");
        }
    }
}