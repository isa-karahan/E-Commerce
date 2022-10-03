using Core.CQRS.Abstract;
using Core.CQRS.Decorators.Caching;
using Core.Utilities.Results;
using DataAccess.Entities;

namespace Bussiness.Products.Queries.GetProducts
{
    public class GetProductsQuery : IQuery<Result<List<Product>>>, ICachableRequest
    {
        public long CategoryId { get; }

        public GetProductsQuery(long categoryId = -1)
        {
            CategoryId = categoryId;
        }
    }
}
