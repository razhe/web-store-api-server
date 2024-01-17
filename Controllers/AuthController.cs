using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Dtos.Accounts;
using web_store_server.Features.Account.Commands;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationResponse>> GetLogin(
            AuthorizationRequest request,
            CancellationToken token)
        {
            var result = await _sender.Send(new AuthorizationCommand(request), token);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("refreshToken")]
        public async Task<ActionResult> GetRefreshToken(
            RefreshTokenRequest request,
            CancellationToken token)
        {
            var result = await _sender.Send(new RefreshTokenCommand(request), token);
            return Ok(result);
        }
    }
}
