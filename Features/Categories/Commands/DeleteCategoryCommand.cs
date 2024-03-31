using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record DeleteCategoryCommand(int CategoryId) : IRequest<Result<bool>>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly StoreContext _context;

        public DeleteCategoryCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productCategory = await _context
                    .ProductCategories
                    .Where(x => x.Id == request.CategoryId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productCategory is null)
                {
                    return new Result<bool>("No se ha encontrado una categoria con ese identificador.");
                }

                productCategory.Active = false;
                productCategory.DeletedAt = DateTimeOffset.Now;

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
