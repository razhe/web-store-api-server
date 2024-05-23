using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Subcategories.Commands
{
    public record DeleteSubcategoryCommand(int SubcategoryId) : IRequest<Result<bool>>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteSubcategoryCommand, Result<bool>>
    {
        private readonly StoreContext _context;

        public DeleteCategoryCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productSubcategory = await _context
                    .ProductCategories
                    .Where(x => x.Id == request.SubcategoryId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productSubcategory is null)
                {
                    return new Result<bool>("No se ha encontrado una subcategoría con ese identificador.");
                }

                productSubcategory.Active = false;
                productSubcategory.DeletedAt = DateTimeOffset.Now;

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
