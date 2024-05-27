using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Subcategories;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Subcategories.Commands
{
    public record UpdateSubcategoryCommand(CreateUpdateSubcategoryDto Subcategory, int SubcategoryId) 
        : IRequest<Result<SubcategoryDto>>;

    public class UpdateSubcategoryCommandHandler : IRequestHandler<UpdateSubcategoryCommand, Result<SubcategoryDto>>
    {
        private readonly StoreContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateSubcategoryCommandHandler(StoreContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<SubcategoryDto>> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productSubcategory = await _dbContext
                    .ProductSubcategories
                    .Where(x => x.Id == request.SubcategoryId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productSubcategory is null)
                {
                    return new Result<SubcategoryDto>("No se ha encontrado una subcategoría con ese identificador.");
                }

                request.Subcategory.MapToModel(productSubcategory);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var subcategory = _mapper.Map<SubcategoryDto>(productSubcategory);

                return new Result<SubcategoryDto>(subcategory);
            }
            catch
            {
                throw;
            }
        }
    }
}
