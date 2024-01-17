using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Common.Exceptions;
using web_store_server.Common.Extensions;
using web_store_server.Domain.Contracts;
using web_store_server.Dtos.Accounts;
using web_store_server.Persistence.Database;
using web_store_server.Shared.Resources;

namespace web_store_server.Features.Account.Commands
{
    public record AuthorizationCommand(AuthorizationRequest AuthorizationRequest) :
        IRequest<AuthorizationResponse>;

    public class AuthorizationCommandHandler
        : IRequestHandler<AuthorizationCommand, AuthorizationResponse>
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AuthorizationCommandHandler(
            StoreContext context,
            IConfiguration configuration,
            IAccountService accountService)
        {
            _context = context;
            _configuration = configuration;
            _accountService = accountService;
        }

        public async Task<AuthorizationResponse> Handle(
            AuthorizationCommand request,
            CancellationToken token)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Email == request.AuthorizationRequest.Email)
                .FirstOrDefaultAsync(cancellationToken: token);

            var oAuthProvider = await _context.OauthProviders
                .AsNoTracking()
                .Where(x =>
                    x.ClientId == request.AuthorizationRequest.ClientId &&
                    x.ClientSecret == request.AuthorizationRequest.ClientSecret)
                .FirstOrDefaultAsync(cancellationToken: token);

            if (oAuthProvider is null)
            {
                throw new RequestException(
                    code: StatusCodes.Status404NotFound,
                    title: "cliente inválido");
            }

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

            return await _accountService.GetAccessTokenAsync(user, oAuthProvider, token);
        }
    }
}
