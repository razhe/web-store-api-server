using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Features.Users.Commands;
using web_store_server.Features.Users.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public UserController(ISender sender, ApiResponseHandler APIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = APIResultHandler;
        }

        /// <summary>
        /// Permite listar los usuarios
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<DefaultAPIResponse<UserDto>>> GetUsersList(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetUsersQuery(), token);

            return _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<IEnumerable<UserDto>>()
                {
                    Message = "Operación realizada con éxito.",
                    IsSuccess = true,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite crear un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DefaultAPIResponse<Guid>>> CreateUsers(
            [FromBody] CreateUpdateUserDto user,
            CancellationToken token)
        {
            var result = await _sender.Send(new CreateUserCommand(user), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<Guid>()
                {
                    Message = "Usuario creado exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<Guid>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite actualizar un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{userId:Guid}")]
        public async Task<ActionResult<DefaultAPIResponse<CreateUpdateUserDto?>>> UpdateUsers(
            [FromRoute] Guid userId,
            [FromBody] CreateUpdateUserDto user,
            CancellationToken token)
        {
            var result = await _sender.Send(new UpdateUserCommand(user, userId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<CreateUpdateUserDto?>()
                {
                    Message = "Usuario actualizado exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<CreateUpdateUserDto?>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }

        /// <summary>
        /// Permite eliminar un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{userId:Guid}")]
        public async Task<ActionResult<DefaultAPIResponse<AnyType>>> RemoveUser(
            [FromRoute] Guid userId,
            CancellationToken token)
        {
            var result = await _sender.Send(new DeleteUserCommand(userId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<CreateUpdateUserDto?>()
                {
                    Message = "Usuario eliminado exitosamente.",
                    IsSuccess = true,
                    Data = null
                }) :
                _APIResultHandler.HandleResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<CreateUpdateUserDto?>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = null
                });
        }
    }
}
