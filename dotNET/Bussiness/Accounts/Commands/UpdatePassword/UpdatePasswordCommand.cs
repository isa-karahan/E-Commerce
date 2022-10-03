using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Accounts.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : ICommand<Result>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
