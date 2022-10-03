using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Products.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<Product> _productRepository;

        public CreateCommentCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productRepository = _unitOfWork.GetRepository<Product>();

            RuleFor(c => c.ProductId).GreaterThan(0)
                .MustAsync(async (id, ct) => await CheckIfProductExists(id))
                .WithMessage("Product not found.");

            RuleFor(c => c.Text).NotEmpty()
                .WithMessage("Comment text must not be empty.");
        }

        private async Task<bool> CheckIfProductExists(long productId)
        {
            var product = await _productRepository.GetById(productId);

            return product != null;
        }
    }
}
