namespace web_store_server.Features.Account
{
    public class AuthorizationResponse
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
