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
        private readonly ApiResponseHandler _errorResultHandler;

        public AccountController(ISender sender, ApiResponseHandler errorResultHandler)
        {
            _sender = sender;
            _errorResultHandler = errorResultHandler;
        }

        /// <summary>
        /// Permite iniciar sesión al usuario
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<CreateAuthorizationDto>> GetLogin(
            GetAuthorizationDto request,
            CancellationToken token)
        {
            var result = await _sender.Send(new AuthorizationCommand(request), token);

            return result.IsSuccess ? 
                Ok(result.Data) : 
                _errorResultHandler.HandleProblemDetailsError(HttpContext, StatusCodes.Status400BadRequest, result.Message);
        }

        /// <summary>
        /// Permite refrescar el token de acceso del usuario
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("refreshToken")]
        public async Task<ActionResult> GetRefreshToken(
            GetRefreshTokenDto request,
            CancellationToken token)
        {
            var result = await _sender.Send(new RefreshTokenCommand(request), token);

            return result.IsSuccess ?
                Ok(result.Data) :
                _errorResultHandler.HandleProblemDetailsError(HttpContext, StatusCodes.Status400BadRequest, result.Message);
        }
    }
}
