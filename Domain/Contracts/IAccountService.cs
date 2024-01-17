using web_store_server.Domain.Entities;
using web_store_server.Dtos.Accounts;

namespace web_store_server.Domain.Contracts
{
    public interface IAccountService
    {
        Task<AuthorizationResponse> GetAccessTokenAsync(
            User user,
            OauthProvider provider,
            CancellationToken token);

        Task<AuthorizationResponse> GetRefreshTokenAsync(
            RefreshTokenRequest request,
            User user,
            OauthProvider provider,
            CancellationToken token);
    }
}
