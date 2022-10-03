using Bussiness.Users.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Queries.GetProfile
{
    public class GetProfileQuery : IQuery<Result<ProfileDto>>
    { }
}
