using web_store_server.Common.Enumerations;

namespace web_store_server.Features.Account
{
    public class AuthorizationRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserEnums.GrantTypes GrantType { get; set; }
    }
}
