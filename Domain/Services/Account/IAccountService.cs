using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Entities;

namespace web_store_server.Domain.Services.Account
{
    public interface IAccountService
    {
        Task<CreateAuthorizationDto> GetAccessTokenAsync(
            User user,
            OauthClient client,
            CancellationToken token);

        Task<CreateAuthorizationDto> GetRefreshTokenAsync(
            GetRefreshTokenDto request,
            User user,
            OauthClient client,
            CancellationToken token);
    }
}
