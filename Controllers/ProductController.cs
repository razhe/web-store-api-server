using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;
using System.Text;
using web_store_mvc.Features.Products.Commands;
using web_store_mvc.Features.Products.Queries;
using web_store_server.Domain.Dtos.Products;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts(
            CancellationToken token)
        {
            

            var result = await _sender.Send(new GetProductsQuery(), token);
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(
            Guid id,
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id), token);
            return StatusCode(200, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            CreateProductDto request,
            CancellationToken token)
        {
            await _sender.Send(new CreateProductCommand(request), token);
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            UpdateProductDto updateProductDto,
            CancellationToken token)
        {
            await _sender.Send(new UpdateProductCommand(updateProductDto), token);
            return StatusCode(204);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(
            Guid id,
            CancellationToken token)
        {
            await _sender.Send(new DeleteProductCommand(id), token);
            return StatusCode(204);
        }
    }
}
