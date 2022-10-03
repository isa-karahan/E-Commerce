using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Commands.DeleteAddress
{
    public class DeleteAddressCommand : ICommand<Result>
    {
        public long AddressId { get; set; }
    }
}
