namespace web_store_server.Dtos.Accounts
{
    public class RefreshTokenRequest
    {
        public string ExpiredToken { get; set; } = null!;
        public string RefreshToken { get; set;} = null!;
    }
}
