using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Features.Categories.Commands;
using web_store_server.Features.Categories.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public CategoryController(ISender sender, ApiResponseHandler aPIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = aPIResultHandler;
        }

        /// <summary>
        /// Permite listar las categorías
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<DefaultAPIResponse<IEnumerable<CategoryDto>>>> GetCategoriesList(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetBrandsQuery(), token);

            return _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<IEnumerable<CategoryDto>>()
                {
                    Message = "Operación realizada con éxito.",
                    IsSuccess = true,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite crear una categoría
        /// </summary>
        /// <param name="category"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DefaultAPIResponse<int>>> CreateCategory(
            [FromBody] CreateUpdateCategoryDto category,
            CancellationToken token)
        {
            var result = await _sender.Send(new CreateCategoryCommand(category), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<int>()
                {
                    Message = "Categoría creada exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<int>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite actualizar una categoría
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="category"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{categoryId:int}")]
        public async Task<ActionResult<DefaultAPIResponse<CategoryDto>>> UpdateCategory(
            [FromRoute] int categoryId,
            [FromBody] CreateUpdateCategoryDto category,
            CancellationToken token)
        {
            var result = await _sender.Send(new UpdateCategoryCommand(category, categoryId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<CategoryDto>()
                {
                    Message = "Categoría actualizada exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<CategoryDto>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite eliminar una categoría
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{categoryId:int}")]
        public async Task<ActionResult<DefaultAPIResponse<AnyType>>> RemoveCategory(
            [FromRoute] int categoryId,
            CancellationToken token)
        {
            var result = await _sender.Send(new DeleteCategoryCommand(categoryId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<AnyType?>()
                {
                    Message = "Categoría eliminada exitosamente.",
                    IsSuccess = true,
                    Data = null
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<AnyType?>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = null
                });
        }
    }
}
