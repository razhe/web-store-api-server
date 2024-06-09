using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Domain.Services.Account
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

        public async Task<CreateAuthorizationDto> GetAccessTokenAsync(
            User user,
            OauthClient client,
            CancellationToken token)
        {
            var accessToken = GenerateAccessToken(user, client);
            var refreshToken = GenerateRefreshToken();

            return await SaveOAuthRequestAsync(client.Id, user.Id, accessToken, refreshToken, token);
        }

        public async Task<CreateAuthorizationDto> GetRefreshTokenAsync(
            GetRefreshTokenDto request,
            User user,
            OauthClient client,
            CancellationToken token)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateAccessToken(user, client);

            return await SaveOAuthRequestAsync(client.Id, user.Id, accessToken, refreshToken, token);
        }

        public async Task<CreateAuthorizationDto> SaveOAuthRequestAsync(
            int clientId,
            Guid userId,
            string accessToken,
            string refreshToken,
            CancellationToken token)
        {
            DateTimeOffset expireOn = DateTimeOffset.Now.AddMinutes(60);
            UserOauthClientRequest oauthRequest = new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ClientId = clientId,
                UserId = userId,
                CreatedAt = DateTimeOffset.Now,
                ExpireOn = expireOn
            };

            await _context.UserOauthClientRequests.AddAsync(oauthRequest, token);
            await _context.SaveChangesAsync(token);

            return new CreateAuthorizationDto { AccessToken = accessToken, RefreshToken = refreshToken, ExpireOn = expireOn };
        }

        public string GenerateRefreshToken()
        {
            byte[] byteArray = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(byteArray);
            var refreshToken = Convert.ToBase64String(byteArray);

            return refreshToken;
        }

        public string GenerateAccessToken(
            User user,
            OauthClient client)
        {
            var key = _configuration.GetValue<string>("JWTSettings:Key");
            var issuer = _configuration.GetValue<string>("JWTSettings:Issuer");
            var audience = _configuration.GetValue<string>("JWTSettings:Audience");

            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.AddClaim(new Claim("ClientIdentifier", client.Id.ToString()));
            claims.AddClaim(new Claim("Name", user.Customer.FirstName.ToString()));
            claims.AddClaim(new Claim("Role", user.Role.ToString()));

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
