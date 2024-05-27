using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
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
    }
}
