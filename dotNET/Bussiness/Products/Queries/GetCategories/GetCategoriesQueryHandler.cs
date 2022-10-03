using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Products.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<List<Category>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<Category> _categoryRepository;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _categoryRepository = _unitOfWork.GetRepository<Category>();
        }
        public async Task<Result<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAll();

            return Result<List<Category>>.Success(categories);
        }
    }
}
