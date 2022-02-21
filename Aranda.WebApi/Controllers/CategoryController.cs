using Aranda.Business.Commands.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aranda.WebApi.Controllers
{
    /// <summary>
    /// This controller contains the definition of the methods associated with the categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator) => _mediator = mediator;
        // POST api/<CategoryController>
        /// <summary>
        /// Exposed method to get the properties
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CategoryCommand categoryCommand)
        {
            CategoryResponse response = await _mediator.Send(categoryCommand);
            return response.InnerContext.Result.Success ? Ok(response) : BadRequest(response.InnerContext.Result);
        }
        [HttpGet("GettAll")]
        public async Task<IActionResult> GettAll()
        {
            CategoryAllResponse response = await _mediator.Send(new CategoryAllCommand());
            return response.InnerContext.Result.Success ? Ok(response.Categories) : BadRequest(response.InnerContext.Result);
        }
    }
}
