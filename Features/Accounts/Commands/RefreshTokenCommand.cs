using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Services.Account;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Accounts.Commands
{
    public record RefreshTokenCommand(GetRefreshTokenDto RefreshTokenRequest) : 
        IRequest<Result<CreateAuthorizationDto>>;

    public class RefreshTokenCommandHandler :
        IRequestHandler<RefreshTokenCommand, Result<CreateAuthorizationDto>>
    {
        private readonly IAccountService _accountService;
        private readonly StoreContext _context;

        public RefreshTokenCommandHandler(IAccountService accountService, StoreContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        public async Task<Result<CreateAuthorizationDto>> Handle(
            RefreshTokenCommand request, 
            CancellationToken token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtHandler.ReadJwtToken(request.RefreshTokenRequest.ExpiredToken);

            if (jwtSecurityToken.ValidTo > DateTimeOffset.Now)
            {
                return new Result<CreateAuthorizationDto>("Error, el token aun no ha expirado");
            }

            var jwtUserId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;
            var jwtClientId = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "ClientIdentifier")?.Value;

            if (jwtUserId is null || jwtClientId is null)
            {
                return new Result<CreateAuthorizationDto>("Error, Token inválido o alterado.");
            }

            var user = await _context.Users
                .Where(x => x.Id == Guid.Parse(jwtUserId))
                .FirstAsync(token);

            var client = await _context.OauthClients
                .Where(x => x.Id == int.Parse(jwtClientId))
                .FirstAsync(token);

            if (user is null)
            {
                return new Result<CreateAuthorizationDto>("Usuario no encontrado, verifica la información");
            }

            var refreshExists = _context.UserOauthClientRequests
                .Where(x =>
                    x.AccessToken == request.RefreshTokenRequest.ExpiredToken &&
                    x.RefreshToken == request.RefreshTokenRequest.RefreshToken)
                .FirstOrDefaultAsync(token);

            if (refreshExists is null)
            {
                return new Result<CreateAuthorizationDto>("No se ha registrado un acceso a nuestro sistema con esas credenciales");
            }
            var response = await _accountService.GetRefreshTokenAsync(request.RefreshTokenRequest, user, client, token);
            return new Result<CreateAuthorizationDto>(response);
        }
    }
}
