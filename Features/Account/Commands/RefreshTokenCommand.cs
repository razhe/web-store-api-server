using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using web_store_server.Common.Exceptions;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Services.Account;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Account.Commands
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

            var jwtUserId = jwtSecurityToken.Claims.First(Claim => Claim.Type == ClaimTypes.NameIdentifier).Value;
            var jwtClientId = jwtSecurityToken.Claims.First(claim => claim.Type == "ClientIdentifier").Value;

            var user = await _context.Users
                .Where(x => x.Id == Guid.Parse(jwtUserId))
                .FirstAsync(token);

            var client = await _context.OauthClients
                .Where(x => x.Id == int.Parse(jwtClientId))
                .FirstAsync(token);

            if (jwtClientId is null || user is null)
            {
                return new Result<CreateAuthorizationDto>("El JWT ha sido modificado o es incorrecto, verifica la información");
            }

            var refreshExists = _context.OauthUserClientRequests
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
