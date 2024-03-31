using AutoMapper;
using MediatR;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record CreateCategoryCommand(ProductCategoryDto ProductCategoryDto) : IRequest;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public CreateCategoryCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ProductCategory productCategory = _mapper.Map<ProductCategoryDto, ProductCategory>(request.ProductCategoryDto);

                _context.Add(productCategory);
                await _context.SaveChangesAsync(cancellationToken);

                return;
            }
            catch
            {
                throw;
            }
        }
    }
}
