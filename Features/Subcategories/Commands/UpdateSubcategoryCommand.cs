using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Subcategories;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Subcategories.Commands
{
    public record UpdateSubcategoryCommand(CreateUpdateSubcategoryDto Subcategory, int SubcategoryId) 
        : IRequest<Result<int>>;

    public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, Result<int>>
    {
        private readonly StoreContext _dbContext;

        public UpdateSubcategoryCommandHandler(StoreContext context)
        {
            _dbContext = context;
        }

        public async Task<Result<int>> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productSubcategory = await _dbContext
                    .ProductSubcategories
                    .Where(x => x.Id == request.SubcategoryId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productSubcategory is null)
                {
                    return new Result<int>("No se ha encontrado una subcategoría con ese identificador.");
                }

                request.Subcategory.MapToModel(productSubcategory);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<int>(productSubcategory.Id);
            }
            catch
            {
                throw;
            }
        }
    }
}
