using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using web_store_server.Common.Exceptions;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Services.Account;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Account.Commands
{
    public record RefreshTokenCommand(GetRefreshTokenDto RefreshTokenRequest) : 
        IRequest<CreateAuthorizationDto>;

    public class RefreshTokenCommandHandler :
        IRequestHandler<RefreshTokenCommand, CreateAuthorizationDto>
    {
        private readonly IAccountService _accountService;
        private readonly StoreContext _context;

        public RefreshTokenCommandHandler(IAccountService accountService, StoreContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        public async Task<CreateAuthorizationDto> Handle(
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
            var jwtClientId = jwtSecurityToken.Claims.First(claim => claim.Type == "ClientIdentifier").Value;

            var user = await _context.Users
                .Where(x => x.Id == Guid.Parse(jwtUserId))
                .FirstAsync(token);

            var client = await _context.OauthClients
                .Where(x => x.Id == int.Parse(jwtClientId))
                .FirstAsync(token);

            if (jwtClientId is null ||
                user is null)
            {
                throw new RequestException(
                    code: StatusCodes.Status400BadRequest,
                    title: "el JWT ha sido modificado o es incorrecto, verifica la información");
            }

            var refreshExists = _context.OauthUserClientRequests
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

            return await _accountService.GetRefreshTokenAsync(request.RefreshTokenRequest, user, client, token);
        }
    }
}
