using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record DeleteCategoryCommand(int CategoryId) : IRequest<Result<bool>>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly StoreContext _dbContext;

        public DeleteCategoryCommandHandler(StoreContext context)
        {
            _dbContext = context;
        }

        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productCategory = await _dbContext
                    .ProductCategories
                    .Where(x => x.Id == request.CategoryId)
                    .Include(x => x.ProductSubcategories)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productCategory is null)
                {
                    return new Result<bool>("No se ha encontrado una categoria con ese identificador.");
                }

                _dbContext.Remove(productCategory); // IAuditable - Soft delete Strategy
                _dbContext.RemoveRange(productCategory.ProductSubcategories); // IAuditable - Soft delete Strategy

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
