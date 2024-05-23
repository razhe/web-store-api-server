using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Commands
{
    public record UpdateProductCommand(CreateUpdateProductDto Product, Guid ProductId) : 
        IRequest<Result<ProductDto>>;

    public class UpdateProductCommandHandler :
        IRequestHandler<UpdateProductCommand, Result<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public UpdateProductCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<ProductDto>> Handle(
            UpdateProductCommand request, 
            CancellationToken token)
        {
            try
            {
                var product = await _context
                .Products
                .Where(x => x.Id == request.ProductId)
                .FirstOrDefaultAsync(token);

                if (product is null)
                {
                    return new Result<ProductDto>("No se ha encontrado un producto con ese identificador.");
                }

                request.Product.MapToModel(product);
                await _context.SaveChangesAsync(token);

                ProductDto productUpdated = _mapper.Map<ProductDto>(product);

                return new Result<ProductDto>(productUpdated);
            }
            catch
            {
                throw;
            }
        }
    }
}
