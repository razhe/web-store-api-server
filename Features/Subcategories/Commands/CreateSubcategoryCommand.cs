using AutoMapper;
using MediatR;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Dtos.Subcategories;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Subcategories.Commands
{
    public record CreateSubcategoryCommand(CreateUpdateSubcategoryDto Subcategory) : IRequest<Result<int>>;

    public class CreateSubcategoryCommandHandler : IRequestHandler<CreateSubcategoryCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public CreateSubcategoryCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task<Result<int>> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ProductSubcategory subcategory = _mapper.Map<CreateUpdateSubcategoryDto, ProductSubcategory>(request.Subcategory);

                _dbContext.Add(subcategory);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<int>(subcategory.Id);
            }
            catch
            {
                throw;
            }
        }
    }
}
