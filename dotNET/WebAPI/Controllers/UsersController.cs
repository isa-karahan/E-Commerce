using Bussiness.Users.Commands.CreateAddress;
using Bussiness.Users.Commands.CreateFavorite;
using Bussiness.Users.Commands.DeleteAddress;
using Bussiness.Users.Commands.DeleteFavorite;
using Bussiness.Users.Commands.DepositMoney;
using Bussiness.Users.Commands.UpdateProfile;
using Bussiness.Users.Dtos;
using Bussiness.Users.Queries.GetAddresses;
using Bussiness.Users.Queries.GetFavorites;
using Bussiness.Users.Queries.GetProfile;
using Bussiness.Users.Queries.GetWalletDetails;
using Core.Utilities.Results;
using DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        { }

        [HttpPost("address")]
        public async Task<Result> AddAddress(CreateAddressCommand createAddressCommand)
        {
            return await Mediator.Send(createAddressCommand);
        }

        [HttpDelete("address")]
        public async Task<Result> DeleteAddress(DeleteAddressCommand deleteAddressCommand)
        {
            return await Mediator.Send(deleteAddressCommand);
        }

        [HttpGet("address")]
        public async Task<Result<List<Address>>> GetAddresses()
        {
            return await Mediator.Send(new GetAddressesQuery());
        }

        [HttpGet("wallet")]
        public async Task<Result<WalletDetailsDto>> GetWalletDetails()
        {
            return await Mediator.Send(new GetWalletDetailsQuery());
        }

        [HttpPut("wallet")]
        public async Task<Result> DepositMoney(DepositMoneyCommand depositMoneyCommand)
        {
            return await Mediator.Send(depositMoneyCommand);
        }

        [HttpPut]
        public async Task<Result> UpdateUser(UpdateProfileCommand updateProfileCommand)
        {
            return await Mediator.Send(updateProfileCommand);
        }

        [HttpGet]
        public async Task<Result<ProfileDto>> GetProfile()
        {
            return await Mediator.Send(new GetProfileQuery());
        }

        [HttpGet("favorites")]
        public async Task<Result<List<FavoriteDto>>> GetFavorites()
        {
            return await Mediator.Send(new GetFavoritesQuery());
        }

        [HttpPost("favorites")]
        public async Task<Result> AddToFavorites(CreateFavoriteCommand createFavoriteCommand)
        {
            return await Mediator.Send(createFavoriteCommand);
        }

        [HttpDelete("favorites")]
        public async Task<Result> DeleteFromFavorites(DeleteFavoriteCommand deleteFavoriteCommand)
        {
            return await Mediator.Send(deleteFavoriteCommand);
        }
    }
}
