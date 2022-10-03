using Core.DataAccess;
using Core.IOC.Accessors;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Users.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommandValidator : AbstractValidator<DeleteFavoriteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Favorite> _favoriteRepository;

        public DeleteFavoriteCommandValidator(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _favoriteRepository = _unitOfWork.GetRepository<Favorite>();

            RuleFor(f => f.ProductId).NotEmpty()
                .MustAsync(async (id, ct) => await CheckIfFavoriteExists(id))
                .WithMessage("Product is not in the list!");
        }

        private async Task<bool> CheckIfFavoriteExists(long productId)
        {
            var fav = await _favoriteRepository.Get(f => f.ProductId == productId
                                             && f.UserId == _contextAccessor.UserId);

            return fav != null;
        }
    }
}
