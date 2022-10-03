using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Commands.CreateFavorite
{
    public class CreateFavoriteCommand : ICommand<Result>
    {
        public long ProductId { get; set; }
    }
}
