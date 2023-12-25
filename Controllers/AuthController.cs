using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
