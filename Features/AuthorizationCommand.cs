using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using web_store_server.Common.Exceptions;
using web_store_server.Common.Extensions;
using web_store_server.Common.Resources;
using web_store_server.Persistence.Database;

namespace web_store_server.Features
{
    public record AuthorizationCommand(AuthorizationRequest AuthorizationRequest) : 
        IRequest<AuthorizationResponse>;

    public class AuthorizationCommandHandler
        : IRequestHandler<AuthorizationCommand, AuthorizationResponse>
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationCommandHandler(
            StoreContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthorizationResponse> Handle(
            AuthorizationCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(x => x.Email == request.AuthorizationRequest.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                throw new RequestException(
                    code: StatusCodes.Status400BadRequest,
                    title: "No existe ningun usuario ligado a ese correo",
                    errors: new List<Error> { new Error(propertyName: "password", errorMessage: "no existe ningun usuario con ese correo, verifique la información") });
            }

            if (user.VerifyPassoword(request.AuthorizationRequest.Password) == false)
            {
                throw new RequestException(
                    code: StatusCodes.Status400BadRequest,
                    title: "Contraseña inválida");
            }

            return GenerateToken(user.Id);
        }

        public AuthorizationResponse GenerateToken(Guid userId)
        {
            var key = _configuration.GetValue<string>("JWTSettings:Key");
            var issuer = _configuration.GetValue<string>("JWTSettings:Issuer");
            var audience = _configuration.GetValue<string>("JWTSettings:Audience");

            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));

            var tokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDuration = DateTimeOffset.Now.DateTime.AddMinutes(60);

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

            AuthorizationResponse tokenResponse = new()
            {
                Token = tokenHandler.WriteToken(tokenConfig),
                ExpiresAt = tokenDuration
            };

            return tokenResponse;
        }
    }
}
