using AutoMapper;
using MediatR;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record CreateCategoryCommand(CreateUpdateCategoryDto CategoryDto) : IRequest<Result<int>>;

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public CreateCategoryCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ProductCategory productCategory = _mapper.Map<CreateUpdateCategoryDto, ProductCategory>(request.CategoryDto);

                _dbContext.Add(productCategory);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<int>(productCategory.Id);
            }
            catch
            {
                throw;
            }
        }
    }
}
