using Bussiness.Products.Dtos;
using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using DataAccess.Entities.Dtos;
using MediatR;

namespace Bussiness.Products.Queries.GetProduct
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Result<ProductDetailsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IEntityRepository<User> _userRepository;

        public GetProductDetailsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _productRepository = _unitOfWork.GetRepository<Product>();
            _categoryRepository = _unitOfWork.GetRepository<Category>();
            _commentRepository = _unitOfWork.GetRepository<Comment>();
            _userRepository = _unitOfWork.GetRepository<User>();
        }
        public async Task<Result<ProductDetailsDto>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId);

            var category = await _categoryRepository.GetById(product.CategoryId);

            var comments = await _commentRepository.GetAll(c => c.ProductId == request.ProductId);

            List<CommentDto> details = new();

            foreach (var comment in comments)
            {
                var user = await _userRepository.GetById(comment.UserId);

                details.Add(new()
                {
                    Added = comment.Added.ToString("G"),
                    Text = comment.Text,
                    Id = comment.Id,
                    UserName = $"{user.FirstName} {user.LastName}"
                });
            }

            ProductDetailsDto result = new()
            {
                Id = product.Id,
                BonusPercentage = category.BonusPercentage,
                Category = category.Name,
                Description = product.Description,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Comments = details
            };

            return Result<ProductDetailsDto>.Success(result);
        }
    }
}