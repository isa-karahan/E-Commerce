using Bussiness.Users.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Queries.GetWalletDetails
{
    public class GetWalletDetailsQuery : IQuery<Result<WalletDetailsDto>>
    { }
}
