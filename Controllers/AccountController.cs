using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Features.Accounts.Commands;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ApiResponseHandler _APIResultHandler;

        public AccountController(ISender sender, ApiResponseHandler APIResultHandler)
        {
            _sender = sender;
            _APIResultHandler = APIResultHandler;
        }

        /// <summary>
        /// Permite iniciar sesión al usuario
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<DefaultAPIResponse<CreateAuthorizationDto>>> GetLogin(
            GetAuthorizationDto request,
            CancellationToken token)
        {
            var result = await _sender.Send(new AuthorizationCommand(request), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<CreateAuthorizationDto>()
                    {
                        IsSuccess = true,
                        Message = "Has iniciado sesión correctamente",
                        Data = result.Data
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status401Unauthorized,
                    new DefaultAPIResponse<CreateAuthorizationDto>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }

        /// <summary>
        /// Permite refrescar el token de acceso del usuario
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("refreshToken")]
        public async Task<ActionResult<DefaultAPIResponse<CreateAuthorizationDto>>> GetRefreshToken(
            GetRefreshTokenDto request,
            CancellationToken token)
        {
            var result = await _sender.Send(new RefreshTokenCommand(request), token);

            return result.IsSuccess ?
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status200OK,
                    new DefaultAPIResponse<CreateAuthorizationDto>()
                    {
                        IsSuccess = true,
                        Message = "Token de acceso refrescado exitosamente.",
                        Data = result.Data
                    }) :
                _APIResultHandler.HandleResponse(
                    StatusCodes.Status400BadRequest,
                    new DefaultAPIResponse<CreateAuthorizationDto>()
                    {
                        IsSuccess = false,
                        Message = result.Message,
                        Data = result.Data
                    });
        }
    }
}
