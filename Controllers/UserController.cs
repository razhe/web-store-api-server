using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace web_store_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
    }
}
