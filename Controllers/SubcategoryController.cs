using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Dtos.Subcategories;
using web_store_server.Features.Categories.Commands;
using web_store_server.Features.Categories.Queries;
using web_store_server.Features.Subcategories.Commands;
using web_store_server.Features.Subcategories.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SubcategoryController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public SubcategoryController(ISender sender, ApiResponseHandler aPIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = aPIResultHandler;
        }

        /// <summary>
        /// Permite listar las subcategorías
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<DefaultAPIResponse<IEnumerable<SubcategoryDto>>>> GetSubcategoriesList(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetSubcategoriesQuery(), token);

            return _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<IEnumerable<SubcategoryDto>>()
                {
                    Message = "Operación realizada con éxito.",
                    IsSuccess = true,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite crear una subcategoría
        /// </summary>
        /// <param name="subcategory"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DefaultAPIResponse<int>>> CreateSubcategory(
            [FromBody] CreateUpdateSubcategoryDto subcategory,
            CancellationToken token)
        {
            var result = await _sender.Send(new CreateSubcategoryCommand(subcategory), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<int>()
                {
                    Message = "Subcategoría creada exitosamente.",
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
        /// Permite actualizar una subcategoría
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <param name="subcategory"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{subcategoryId:int}")]
        public async Task<ActionResult<DefaultAPIResponse<SubcategoryDto>>> UpdateSubcategory(
            [FromRoute] int subcategoryId,
            [FromBody] CreateUpdateSubcategoryDto subcategory,
            CancellationToken token)
        {
            var result = await _sender.Send(new UpdateSubcategoryCommand(subcategory, subcategoryId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<SubcategoryDto>()
                {
                    Message = "Subcategoría actualizada exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<SubcategoryDto>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite eliminar una subcategoría
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{subcategoryId:int}")]
        public async Task<ActionResult<DefaultAPIResponse<AnyType>>> RemoveSubcategory(
            [FromRoute] int subcategoryId,
            CancellationToken token)
        {
            var result = await _sender.Send(new DeleteSubcategoryCommand(subcategoryId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<AnyType?>()
                {
                    Message = "Subcategoría eliminada exitosamente.",
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
