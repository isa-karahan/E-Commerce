using Bussiness.Accounts.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Accounts.Queries.Login
{
    public class LoginQuery : IQuery<Result<LoginInfo>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
