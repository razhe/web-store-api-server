using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Subcategories;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Subcategories.Queries
{
    public record GetSubcategoriesQuery : IRequest<Result<IEnumerable<SubcategoryDto>>>;

    public class GetSubcategoriesQueryHandler : IRequestHandler<GetSubcategoriesQuery, Result<IEnumerable<SubcategoryDto>>>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public GetSubcategoriesQueryHandler(IMapper mapper, StoreContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<SubcategoryDto>>> Handle(GetSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _context
                    .ProductSubcategories
                    .Where(p => !p.IsDeleted)
                    .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<SubcategoryDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
