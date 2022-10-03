using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Products.Commands.CreateComment
{
    public class CreateCommentCommand : ICommand<Result>
    {
        public long ProductId { get; set; }
        public string Text { get; set; }
    }
}
