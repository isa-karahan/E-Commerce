using Bussiness.Products.Commands.CreateComment;
using Bussiness.Products.Dtos;
using Bussiness.Products.Queries.GetCategories;
using Bussiness.Products.Queries.GetProduct;
using Bussiness.Products.Queries.GetProducts;
using Core.Utilities.Results;
using DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        { }

        [HttpGet]
        public async Task<Result<List<Product>>> GetAllProducts()
        {
            return await Mediator.Send(new GetProductsQuery());
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<Result<List<Product>>> GetAllProductsByCategoryId([FromRoute] long categoryId)
        {
            return await Mediator.Send(new GetProductsQuery(categoryId));
        }

        [HttpGet("categories")]
        public async Task<Result<List<Category>>> GetAllCategories()
        {
            return await Mediator.Send(new GetCategoriesQuery());
        }

        [HttpGet("{id}")]
        public async Task<Result<ProductDetailsDto>> GetProductById([FromRoute] long id)
        {
            return await Mediator.Send(new GetProductDetailsQuery(id));
        }

        [Authorize]
        [HttpPost("comment")]
        public async Task<Result> AddComment(CreateCommentCommand createCommentCommand)
        {
            return await Mediator.Send(createCommentCommand);
        }
    }
}
