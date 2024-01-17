namespace web_store_server.Dtos.Accounts
{
    public class AuthorizationResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTimeOffset ExpireOn { get; set; }
    }
}
