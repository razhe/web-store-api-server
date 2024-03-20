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
        private readonly StoreContext _context;

        public DeleteProductCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(
            DeleteProductCommand request,
            CancellationToken token)
        {
            var product = await _context
                .Products
                .Where(x => x.Id == request.ProductId)
                .FirstOrDefaultAsync(token);

            if (product is null)
            {
                return new Result<bool>("No se ha encontrado un producto con ese identificador.");
            }

            product.Active = false;
            product.DeletedAt = DateTimeOffset.Now;

            await _context.SaveChangesAsync(token);

            return new Result<bool>(true);
        }
    }
}
