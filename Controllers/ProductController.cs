﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;
using System.Text;
using web_store_mvc.Features.Products.Commands;
using web_store_mvc.Features.Products.Queries;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Products;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ErrorResultHandler __errorResultHandler;

        public ProductController(ISender sender, ErrorResultHandler errorResultHandler)
        {
            _sender = sender;
            __errorResultHandler = errorResultHandler;
        }

        /// <summary>
        /// Lista todos los productos
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductsQuery(), token);
            return Ok(result.Data);
        }

        /// <summary>
        /// Permite obtener un producto especifico
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GetProductDto>> GetProductById(
            Guid id,
            CancellationToken token)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id), token);
            return Ok(result.Data);
        }

        /// <summary>
        /// Permite crear un producto
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            CreateProductDto request,
            CancellationToken token)
        {
            await _sender.Send(new CreateProductCommand(request), token);
            return Ok();
        }

        /// <summary>
        /// Permite actualizar un producto
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(
            UpdateProductDto updateProductDto,
            CancellationToken token)
        {
            await _sender.Send(new UpdateProductCommand(updateProductDto), token);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(
            Guid id,
            CancellationToken token)
        {
            await _sender.Send(new DeleteProductCommand(id), token);
            return Ok();
        }
    }
}
