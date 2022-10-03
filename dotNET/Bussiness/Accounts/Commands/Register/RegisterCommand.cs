using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Accounts.Commands.Register
{
    public class RegisterCommand : ICommand<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
