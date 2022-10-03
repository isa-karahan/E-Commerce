using Bussiness.Users.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Queries.GetFavorites
{
    public class GetFavoritesQuery : IQuery<Result<List<FavoriteDto>>>
    { }
}
