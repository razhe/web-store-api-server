using web_store_server.Domain.Entities;
using web_store_server.Dtos.Accounts;

namespace web_store_server.Domain.Contracts
{
    public interface IAccountService
    {
        Task<AuthorizationResponse> GetAccessTokenAsync(
            User user,
            OauthClient client,
            CancellationToken token);

        Task<AuthorizationResponse> GetRefreshTokenAsync(
            RefreshTokenRequest request,
            User user,
            OauthClient client,
            CancellationToken token);
    }
}
