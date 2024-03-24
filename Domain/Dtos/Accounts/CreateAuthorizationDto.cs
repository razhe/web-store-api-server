namespace web_store_server.Domain.Dtos.Accounts
{
    public class CreateAuthorizationDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTimeOffset ExpireOn { get; set; }
    }
}
