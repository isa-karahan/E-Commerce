using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Users.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommand : ICommand<Result>
    {
        public long ProductId { get; set; }
    }
}
