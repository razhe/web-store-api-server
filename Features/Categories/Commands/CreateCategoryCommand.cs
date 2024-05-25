using AutoMapper;
using MediatR;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record CreateCategoryCommand(CreateUpdateCategoryDto ProductCategoryDto) : IRequest;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public CreateCategoryCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ProductCategory productCategory = _mapper.Map<CreateUpdateCategoryDto, ProductCategory>(request.ProductCategoryDto);

                _dbContext.Add(productCategory);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return;
            }
            catch
            {
                throw;
            }
        }
    }
}
