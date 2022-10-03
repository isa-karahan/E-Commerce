using Core.CQRS.Abstract;
using Core.Utilities.Results;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Queries.GetAddresses
{
    public class GetAddressesQuery : IQuery<Result<List<Address>>>
    {}
}
