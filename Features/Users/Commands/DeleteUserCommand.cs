using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Commands
{
    public record DeleteUserCommand(Guid UserId) : IRequest<Result<bool>>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly StoreContext _context;

        public DeleteUserCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context
                    .Users
                    .Where(x => x.Id == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user is null)
                {
                    return new Result<bool>("No se ha encontrado el usuario con ese identificador.");
                }

                user.Active = false;
                user.DeletedAt = DateTimeOffset.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return new Result<bool>(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
