using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Features.Users.Commands;
using web_store_server.Features.Users.Queries;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public UserController(ISender sender, ApiResponseHandler APIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = APIResultHandler;
        }

        [HttpGet]
        public async Task<ActionResult<DefaultAPIResponse<UserDto>>> GetUsersList(
            CancellationToken token)
        {
            var result = await _sender.Send(new GetUsersQuery(), token);

            return _APIResultHandler.HandleDefaultResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<IEnumerable<UserDto>>()
                {
                    Message = "Operación realizada con éxito.",
                    IsSuccess = true,
                    Data = result.Data
                });
        }

        [HttpPost]
        public async Task<ActionResult<DefaultAPIResponse<Guid>>> CreateUsers(
            [FromBody] CreateUpdateUserDto user,
            CancellationToken token)
        {
            var result = await _sender.Send(new CreateUserCommand(user), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleDefaultResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<Guid>()
                {
                    Message = "Usuario creado exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleDefaultResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<Guid>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }

        [HttpPut("{userId:Guid}")]
        public async Task<ActionResult<DefaultAPIResponse<CreateUpdateUserDto?>>> UpdateUsers(
            [FromRoute] Guid userId,
            [FromBody] CreateUpdateUserDto user,
            CancellationToken token)
        {
            var result = await _sender.Send(new UpdateUserCommand(user, userId), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleDefaultResponse(
                StatusCodes.Status200OK,
                new DefaultAPIResponse<CreateUpdateUserDto?>()
                {
                    Message = "Usuario actualizado exitosamente.",
                    IsSuccess = true,
                    Data = result.Data
                }) :
                _APIResultHandler.HandleDefaultResponse(
                StatusCodes.Status400BadRequest,
                new DefaultAPIResponse<CreateUpdateUserDto?>()
                {
                    Message = result.Message,
                    IsSuccess = false,
                    Data = result.Data
                });
        }
    }
}
