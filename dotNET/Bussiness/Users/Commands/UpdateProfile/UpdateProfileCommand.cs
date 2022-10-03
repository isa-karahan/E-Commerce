using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Commands.UpdateProfile
{
    public class UpdateProfileCommand : ICommand<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
