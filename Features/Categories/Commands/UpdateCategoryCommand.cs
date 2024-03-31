using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record UpdateCategoryCommand(ProductCategoryDto ProductCategoryDto, int CategoryId) : 
        IRequest<Result<GetProductCategoryDto>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<GetProductCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public UpdateCategoryCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<GetProductCategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productCategory = await _context
                    .ProductCategories
                    .Where(x => x.Id == request.CategoryId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productCategory is null)
                {
                    return new Result<GetProductCategoryDto>("No se ha encontrado una categoría con ese identificador.");
                }

                request.ProductCategoryDto.MapToModel(productCategory);
                await _context.SaveChangesAsync(cancellationToken);

                GetProductCategoryDto categoryUpdated = _mapper.Map<GetProductCategoryDto>(productCategory);

                return new Result<GetProductCategoryDto>(categoryUpdated);
            }
            catch
            {
                throw;
            }
        }
    }
}
