using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Queries
{
    public record GetProductsQuery :
        IRequest<Result<IEnumerable<GetProductDto>>>;

    public class GetProductsQueryHandler :
        IRequestHandler<GetProductsQuery, Result<IEnumerable<GetProductDto>>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public GetProductsQueryHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<IEnumerable<GetProductDto>>> Handle(
            GetProductsQuery request,
            CancellationToken token)
        {
            try
            {
                var response = await _context
                    .Products
                    .Where(p => !p.IsDeleted)
                    .ProjectTo<GetProductDto>(_mapper.ConfigurationProvider).ToArrayAsync(token);

                return new Result<IEnumerable<GetProductDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
