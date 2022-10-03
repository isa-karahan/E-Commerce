using Core.DataAccess;
using Core.IOC.Accessors;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Users.Commands.CreateFavorite
{
    public class CreateFavoriteCommandValidator : AbstractValidator<CreateFavoriteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Favorite> _favoriteRepository;

        public CreateFavoriteCommandValidator(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _productRepository = _unitOfWork.GetRepository<Product>();
            _favoriteRepository = _unitOfWork.GetRepository<Favorite>();


            RuleFor(f => f.ProductId).NotEmpty()
                .MustAsync(async (id, ct) => await CheckIfProductExists(id))
                .WithMessage("Product not found.")
                .MustAsync(async (id, ct) => await CheckIfFavoriteAdded(id))
                .WithMessage("Product is already in the list!");
        }

        private async Task<bool> CheckIfProductExists(long productId)
        {
            var product = await _productRepository.GetById(productId);

            return product != null;
        }

        private async Task<bool> CheckIfFavoriteAdded(long productId)
        {
            var check = await _favoriteRepository.Get(f => f.ProductId == productId
                                                        && f.UserId == _contextAccessor.UserId);

            return check == null;
        }
    }
}
