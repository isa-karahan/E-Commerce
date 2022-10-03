using Core.CQRS.Abstract;
using Core.CQRS.Decorators.Caching;
using Core.Utilities.Results;
using DataAccess.Entities;

namespace Bussiness.Products.Queries.GetCategories
{
    public class GetCategoriesQuery : IQuery<Result<List<Category>>>, ICachableRequest
    { }
}
