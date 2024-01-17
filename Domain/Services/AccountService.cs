using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using web_store_server.Domain.Contracts;
using web_store_server.Domain.Entities;
using web_store_server.Dtos.Accounts;
using web_store_server.Persistence.Database;

namespace web_store_server.Domain.Services
{
    public class AccountService :
        IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly StoreContext _context;

        public AccountService(IConfiguration configuration, StoreContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthorizationResponse> GetAccessTokenAsync(
            User user,
            OauthProvider provider,
            CancellationToken token)
        {
            var accessToken = GenerateAccessToken(user, provider);
            var refreshToken = GenerateRefreshToken();

            return await SaveOAuthRequestAsync(provider.Id, user.Id, accessToken, refreshToken, token);
        }

        public async Task<AuthorizationResponse> GetRefreshTokenAsync(
            RefreshTokenRequest request,
            User user,
            OauthProvider provider,
            CancellationToken token)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateAccessToken(user, provider);

            return await SaveOAuthRequestAsync(provider.Id, user.Id, accessToken, refreshToken, token);
        }

        public async Task<AuthorizationResponse> SaveOAuthRequestAsync(
            int providerId,
            Guid userId,
            string accessToken,
            string refreshToken,
            CancellationToken token)
        {
            DateTimeOffset expireOn = DateTimeOffset.Now.AddMinutes(60);
            UserOauthRequest oauthRequest = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ProviderId = providerId,
                UserId = userId,
                CreatedAt = DateTimeOffset.Now,
                ExpireOn = expireOn
            };

            await _context.UserOauthRequests.AddAsync(oauthRequest, token);
            await _context.SaveChangesAsync(token);

            return new AuthorizationResponse { AccessToken = accessToken, RefreshToken = refreshToken, ExpireOn = expireOn };
        }

        public string GenerateRefreshToken()
        {
            byte[] byteArray = new byte[64];
            string refreshToken = "";

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(byteArray);
            refreshToken = Convert.ToBase64String(byteArray);

            return refreshToken;
        }

        public string GenerateAccessToken(
            User user,
            OauthProvider provider)
        {
            var key = _configuration.GetValue<string>("JWTSettings:Key");
            var issuer = _configuration.GetValue<string>("JWTSettings:Issuer");
            var audience = _configuration.GetValue<string>("JWTSettings:Audience");

            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("UserIdentifier", user.Id.ToString()));
            claims.AddClaim(new Claim("ProviderIdentifier", provider.Id.ToString()));

            var tokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDuration = DateTime.UtcNow.AddMinutes(60);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = claims,
                Expires = tokenDuration,
                SigningCredentials = tokenCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenConfig);
        }
    }
}
