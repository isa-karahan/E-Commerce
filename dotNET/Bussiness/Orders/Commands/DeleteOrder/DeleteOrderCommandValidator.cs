using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityRepository<OrderDetail> _orderDetailRepository;

        public DeleteOrderCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderDetailRepository = _unitOfWork.GetRepository<OrderDetail>();

            RuleFor(o => o.OrderId).NotEmpty()
                .MustAsync(async (id, ct) => await CheckIfOrderExists(id))
                .WithMessage("Order not found.");
        }

        private async Task<bool> CheckIfOrderExists(long orderId)
        {
            var od = await _orderDetailRepository.GetById(orderId);

            return od != null;
        }
    }
}
