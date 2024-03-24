namespace web_store_server.Domain.Dtos.Accounts
{
    public class GetRefreshTokenDto
    {
        public string ExpiredToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
