using Core.DataAccess;
using Core.IOC.Accessors;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Entities;
using MediatR;

namespace Bussiness.Products.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IEntityRepository<User> _userRepository;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _productRepository = _unitOfWork.GetRepository<Product>();
            _commentRepository = _unitOfWork.GetRepository<Comment>();
            _userRepository = _unitOfWork.GetRepository<User>();
        }
        public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var newComment = new Comment
            {
                ProductId = request.ProductId,
                Text = request.Text,
                UserId = _contextAccessor.UserId
            };

            await _commentRepository.Add(newComment);
            await _unitOfWork.SaveAsync();

            return Result.Success("Comment added successfully");
        }
    }
}
