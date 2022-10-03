using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<List<Product>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<Product> _productRepository;

        public GetProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _productRepository = _unitOfWork.GetRepository<Product>();
        }
        public async Task<Result<List<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = request.CategoryId == -1 ? await _productRepository.GetAll() : 
                await _productRepository.GetAll(p => p.CategoryId == request.CategoryId);

            return Result<List<Product>>.Success(result);
        }
    }
}
