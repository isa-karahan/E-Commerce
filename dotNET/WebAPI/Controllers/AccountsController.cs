using Bussiness.Accounts.Commands.DeleteAccount;
using Bussiness.Accounts.Commands.GetNewToken;
using Bussiness.Accounts.Commands.Logout;
using Bussiness.Accounts.Commands.Register;
using Bussiness.Accounts.Commands.UpdatePassword;
using Bussiness.Accounts.Dtos;
using Bussiness.Accounts.Queries.Login;
using Core.Utilities.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AccountsController : BaseController
    {
        public AccountsController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("login")]
        public async Task<Result<LoginInfo>> Login(LoginQuery loginQuery)
        {
            return await Mediator.Send(loginQuery);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<Result> Logout()
        {
            return await Mediator.Send(new LogoutCommand());
        }

        [HttpPost("register")]
        public async Task<Result> Register(RegisterCommand registerCommand)
        {
            return await Mediator.Send(registerCommand);
        }

        [Authorize]
        [HttpPut]
        public async Task<Result> UpdatePassword(UpdatePasswordCommand updatePasswordCommand)
        {
            return await Mediator.Send(updatePasswordCommand);
        }

        [Authorize]
        [HttpDelete]
        public async Task<Result> DeleteAccount()
        {
            return await Mediator.Send(new DeleteAccountCommand());
        }

        [HttpPost("token")]
        public async Task<Result> GetNewAccessToken()
        {
            return await Mediator.Send(new GetNewTokenCommand());
        }
    }
}
