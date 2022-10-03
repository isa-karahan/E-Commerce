using Bussiness.Products.Dtos;
using Core.CQRS.Abstract;
using Core.Utilities.Results;

namespace Bussiness.Products.Queries.GetProduct
{
    public class GetProductDetailsQuery : IQuery<Result<ProductDetailsDto>>
    {
        public long ProductId { get; }

        public GetProductDetailsQuery(long productId)
        {
            ProductId = productId;
        }
    }
}
