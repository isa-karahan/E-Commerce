using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Users.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Favorite> _favoriteRepository;

        public DeleteFavoriteCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _productRepository = _unitOfWork.GetRepository<Product>();
            _favoriteRepository = _unitOfWork.GetRepository<Favorite>();
        }
        public async Task<Result> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId);

            var check = await _favoriteRepository.Get(f => f.ProductId == request.ProductId
                                                        && f.UserId == _contextAccessor.UserId);

            await _favoriteRepository.Delete(check);

            await _unitOfWork.SaveAsync();

            return Result.Success($"{product.Name} removed from the favorites list.");
        }
    }
}
