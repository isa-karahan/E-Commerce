using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Commands.CreateFavorite
{
    public class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Favorite> _favoriteRepository;

        public CreateFavoriteCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _productRepository = _unitOfWork.GetRepository<Product>();
            _favoriteRepository = _unitOfWork.GetRepository<Favorite>();
        }
        public async Task<Result> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId);

            await _favoriteRepository.Add(new()
            {
                ProductId = request.ProductId,
                UserId = _contextAccessor.UserId
            });

            await _unitOfWork.SaveAsync();

            return Result.Success($"{product.Name} added to favorites list.");
        }
    }
}
