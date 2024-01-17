using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using web_store_server.Common.Exceptions;
using web_store_server.Domain.Contracts;
using web_store_server.Dtos.Accounts;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Account.Commands
{
    public record RefreshTokenCommand(RefreshTokenRequest RefreshTokenRequest) : 
        IRequest<AuthorizationResponse>;

    public class RefreshTokenCommandHandler :
        IRequestHandler<RefreshTokenCommand, AuthorizationResponse>
    {
        private readonly IAccountService _accountService;
        private readonly StoreContext _context;

        public RefreshTokenCommandHandler(IAccountService accountService, StoreContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        public async Task<AuthorizationResponse> Handle(
            RefreshTokenCommand request, 
            CancellationToken token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtHandler.ReadJwtToken(request.RefreshTokenRequest.ExpiredToken);

            if (jwtSecurityToken.ValidTo > DateTimeOffset.Now)
            {
                throw new RequestException(
                    code: StatusCodes.Status400BadRequest,
                    title: "el Token aún no ha expirado");
            }

            var jwtUserId = jwtSecurityToken.Claims.First(claim => claim.Type == "UserIdentifier").Value;
            var jwtProviderId = jwtSecurityToken.Claims.First(claim => claim.Type == "ProviderIdentifier").Value;

            var user = await _context.Users
                .Where(x => x.Id == Guid.Parse(jwtUserId))
                .FirstAsync(token);

            var provider = await _context.OauthProviders
                .Where(x => x.Id == int.Parse(jwtProviderId))
                .FirstAsync(token);

            if (provider is null ||
                user is null)
            {
                throw new RequestException(
                    code: StatusCodes.Status400BadRequest,
                    title: "el JWT ha sido modificado o es incorrecto, verifica la información");
            }

            var refreshExists = _context.UserOauthRequests
                .Where(x =>
                    x.AccessToken == request.RefreshTokenRequest.ExpiredToken &&
                    x.RefreshToken == request.RefreshTokenRequest.RefreshToken)
                .FirstOrDefaultAsync(token);

            if (refreshExists is null)
            {
                throw new RequestException(
                    code: StatusCodes.Status400BadRequest,
                    title: "No se ha registrado un acceso a nuestro sistema con esas credenciales");
            }

            return await _accountService.GetRefreshTokenAsync(request.RefreshTokenRequest, user, provider, token);
        }
    }
}
