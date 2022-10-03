using Bussiness.Orders.Dtos;
using Core.DataAccess;
using Core.IOC.Accessors;
using DataAccess.Abstract;
using DataAccess.Entities;
using FluentValidation;

namespace Bussiness.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextAccessor _contextAccessor;

        private readonly IEntityRepository<Wallet> _walletRepository;
        private readonly IEntityRepository<Product> _productRepository;
        private readonly IEntityRepository<Address> _addressRepository;

        public CreateOrderCommandValidator(IUnitOfWork unitOfWork, IUserContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

            _walletRepository = _unitOfWork.GetRepository<Wallet>();
            _productRepository = _unitOfWork.GetRepository<Product>();
            _addressRepository = _unitOfWork.GetRepository<Address>();

            RuleFor(order => order.OrderItems).NotEmpty().WithMessage("Items must not be empty.")
                    .Must(item => item.All(o => o.Quantity > 0))
                    .WithMessage("Quantity must be positive.")
                    .MustAsync(async (items, ct) => await CheckProducts(items))
                    .WithMessage("Invalid order item input.")
                    .MustAsync(async (items, ct) => await CheckProductStocks(items))
                    .WithMessage("Insufficient stock.")
                    .MustAsync(async (items, ct) => await CheckIfUserHaveEnoughBalance(items))
                    .WithMessage("Insufficient balance.");

            RuleFor(order => order.AddressId).
                MustAsync(async (addressId, ct) => await CheckIfAddressExists(addressId))
                .WithMessage("Address not found.");
        }

        private async Task<bool> CheckProducts(List<CreateOrderItemDto> orderItems)
        {

            foreach (var item in orderItems)
            {
                var product = await _productRepository.GetById(item.Product);

                if (product is null)
                    return false;
            }
            return true;
        }

        private async Task<bool> CheckProductStocks(List<CreateOrderItemDto> orderItems)
        {
            foreach (var item in orderItems)
            {
                var product = await _productRepository.GetById(item.Product);

                if (product.UnitsInStock < item.Quantity)
                    return false;
            }

            return true;
        }

        private async Task<bool> CheckIfUserHaveEnoughBalance(List<CreateOrderItemDto> orderItems)
        {
            int totalPrice = 0;

            foreach (var item in orderItems)
            {
                var product = await _productRepository.GetById(item.Product);
                totalPrice += item.Quantity * product.UnitPrice;
            }

            var wallet = await _walletRepository.Get(w => w.UserId == _contextAccessor.UserId);

            if (wallet is null)
                return false;

            if (wallet.Balance < totalPrice)
                return false;

            return true;
        }

        private async Task<bool> CheckIfAddressExists(long addressId)
        {
            var address = await _addressRepository.GetById(addressId);

            return address != null;
        }
    }
}
