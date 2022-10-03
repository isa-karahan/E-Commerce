using Bussiness.Users.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Queries.GetFavorites
{
    public class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, Result<List<FavoriteDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;


        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Favorite> _favoriteRepository;

        public GetFavoritesQueryHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _productRepository = _unitOfWork.GetRepository<Product>();
            _favoriteRepository = _unitOfWork.GetRepository<Favorite>();
        }
        public async Task<Result<List<FavoriteDto>>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
        {
            var favorites = await _favoriteRepository.GetAll(f => f.UserId == _contextAccessor.UserId);

            var result = new List<FavoriteDto>();

            foreach (var favorite in favorites)
            {
                var product = await _productRepository.GetById(favorite.ProductId);
                result.Add(new()
                {
                    Added = favorite.Added.ToString("G"),
                    ProductId = favorite.ProductId,
                    Id = favorite.Id,
                    ProductName = product.Name,
                    UnitPrice = product.UnitPrice
                });
            }

            return Result<List<FavoriteDto>>.Success(result);
        }
    }
}
