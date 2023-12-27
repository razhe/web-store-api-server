using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Features;

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

        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationResponse>> GetLogin(
            AuthorizationRequest request,
            CancellationToken token)
        {
            var result = await _sender.Send(new AuthorizationCommand(request));
            return Ok(result);
        }
    }
}
