using Aranda.Business.Commands.Products;
using Aranda.Common.Types.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aranda.WebApi.Controllers
{
    /// <summary>
    /// This controller contains the definition of the methods associated with the products.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) => _mediator = mediator;
        // POST api/<ProductsController>
        /// <summary>
        /// Exposed method to get the products
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]ProductCommand productCommand)
        {
            ProductResponse response = await _mediator.Send(productCommand);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        [HttpPost("CreateImages")]
        public async Task<IActionResult> CreateImages([FromBody]ProductImagesCommand productImagesCommand)
        {
            ProductImagesResponse response = await _mediator.Send(productImagesCommand);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        [HttpGet("GetAllImages{productId}")]
        public async Task<IActionResult> GetAllImages(Guid productId)
        {
            TransactionalProductResponse response = await _mediator.Send(new TransactionalProductCommand()
            {
                ProccessType = Common.Generics.Proccesors.GetAllImages,
                ProductRequest = new()
                {
                    ProductId = productId
                }
            });
            return response.InnerContext.Result.Success ? Ok(response.ProductImages) : BadRequest(response.InnerContext.Result);
        }
        [HttpGet("GettAll")]
        public async Task<IActionResult> GettAll()
        {
            TransactionalProductResponse response = await _mediator.Send(new TransactionalProductCommand()
            {
                ProccessType = Common.Generics.Proccesors.GetAll
            });
            return response.InnerContext.Result.Success ? Ok(response.ProductList) : BadRequest(response.InnerContext.Result);
        }
        [HttpGet("Find{productId}")]
        public async Task<IActionResult> Find(Guid productId)
        {
            TransactionalProductResponse response = await _mediator.Send(new TransactionalProductCommand()
            {
                ProductRequest = new()
                {
                    ProductId = productId
                },
                ProccessType = Common.Generics.Proccesors.Find
            });
            return response.InnerContext.Result.Success ? Ok(response.ProductResponse) : BadRequest(response.InnerContext.Result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            TransactionalProductResponse response = await _mediator.Send(new TransactionalProductCommand()
            {
                ProccessType = Common.Generics.Proccesors.Delete,
                ProductRequest = new(){ ProductId = id }
            });
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> Update(Guid categoryId, [FromBody]Product product)
        {
            TransactionalProductCommand transactionalProductCommand = new()
            {
                ProductRequest = product
            };
            transactionalProductCommand.CategoryId = categoryId;
            transactionalProductCommand.ProccessType = Common.Generics.Proccesors.Update;
            TransactionalProductResponse response = await _mediator.Send(transactionalProductCommand);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
    }
}
