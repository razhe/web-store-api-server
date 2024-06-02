using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Brands;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Brand.Queries
{
    public record GetBrandsQuery : IRequest<Result<IEnumerable<BrandDto>>>;

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, Result<IEnumerable<BrandDto>>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public GetBrandsQueryHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task<Result<IEnumerable<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _dbContext
                    .ProductBrands
                    .Where(p => !p.IsDeleted)
                    .ProjectTo<BrandDto>(_mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<BrandDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
