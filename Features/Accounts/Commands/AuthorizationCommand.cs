using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Common.Extensions;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Accounts;
using web_store_server.Domain.Services.Account;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Accounts.Commands
{
    public record AuthorizationCommand(GetAuthorizationDto AuthorizationRequest) :
        IRequest<Result<CreateAuthorizationDto>>;

    public class AuthorizationCommandHandler
        : IRequestHandler<AuthorizationCommand, Result<CreateAuthorizationDto>>
    {
        private readonly StoreContext _context;
        private readonly IAccountService _accountService;

        public AuthorizationCommandHandler(
            StoreContext context,
            IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<Result<CreateAuthorizationDto>> Handle(
            AuthorizationCommand request,
            CancellationToken token)
        {
            try
            {
                var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Email == request.AuthorizationRequest.Email)
                .FirstOrDefaultAsync(cancellationToken: token);

                var oAuthClient = await _context.OauthClients
                    .AsNoTracking()
                    .Where(x =>
                        x.ClientId == request.AuthorizationRequest.ClientId &&
                        x.ClientSecret == request.AuthorizationRequest.ClientSecret)
                    .FirstOrDefaultAsync(cancellationToken: token);

                if (oAuthClient is null)
                {
                    return new Result<CreateAuthorizationDto>("Error, cliente inválido.");
                }

                if (user is null)
                {
                    return new Result<CreateAuthorizationDto>("no existe ningun usuario con ese correo, verifique la información");
                }

                if (user.VerifyPassoword(request.AuthorizationRequest.Password) == false)
                {
                    return new Result<CreateAuthorizationDto>("Contraseña inválida");
                }

                var response = await _accountService.GetAccessTokenAsync(user, oAuthClient, token);
                return new Result<CreateAuthorizationDto>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
