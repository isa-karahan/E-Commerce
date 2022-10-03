using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Commands.CreateAddress
{
    public class CreateAddressCommand : ICommand<Result>
    {
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
    }
}
