using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Commands
{
    public record DeleteProductCommand(Guid ProductId)
        : IRequest<Result<bool>>;

    public class DeleteProductCommandHandler :
        IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly StoreContext _dbContext;

        public DeleteProductCommandHandler(StoreContext context)
        {
            _dbContext = context;
        }

        public async Task<Result<bool>> Handle(
            DeleteProductCommand request,
            CancellationToken token)
        {
            try
            {
                var product = await _dbContext
                .Products
                .Where(x => x.Id == request.ProductId)
                .FirstOrDefaultAsync(token);

                if (product is null)
                {
                    return new Result<bool>("No se ha encontrado un producto con ese identificador.");
                }

                _dbContext.Remove(product); // IAuditable - Soft delete Strategy
                await _dbContext.SaveChangesAsync(token);

                return new Result<bool>(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
