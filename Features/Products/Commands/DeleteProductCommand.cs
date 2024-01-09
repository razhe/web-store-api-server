using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Commands
{
    public record DeleteProductCommand(Guid ProductId)
        : IRequest;

    public class DeleteProductCommandHandler :
        IRequestHandler<DeleteProductCommand>
    {
        private readonly StoreContext _context;

        public DeleteProductCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task Handle(
            DeleteProductCommand request,
            CancellationToken token)
        {
            var product = await _context
                .Products
                .Where(x => x.Id == request.ProductId)
                .FirstAsync(token);

            product.Active = false;
            product.DeletedAt = DateTimeOffset.Now;

            await _context.SaveChangesAsync(token);

            return;
        }
    }
}
