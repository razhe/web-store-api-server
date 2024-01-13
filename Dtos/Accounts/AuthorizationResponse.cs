namespace web_store_server.Dtos.Accounts
{
    public class AuthorizationResponse
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
