namespace web_store_server.Features
{
    public class AuthorizationRequest
    {
        public enum GrantTypes
        {
            password
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public GrantTypes GrantType { get; set; }
    }
}
