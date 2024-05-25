using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Commands
{
    public record DeleteUserCommand(Guid UserId) : IRequest<Result<bool>>;

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<bool>>
    {
        private readonly StoreContext _dbContext;

        public DeleteUserCommandHandler(StoreContext context)
        {
            _dbContext = context;
        }

        public async Task<Result<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext
                    .Users
                    .Where(x => x.Id == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user is null)
                {
                    return new Result<bool>("No se ha encontrado el usuario con ese identificador.");
                }

                _dbContext.Remove(user); // IAuditable - Soft delete Strategy
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<bool>(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
