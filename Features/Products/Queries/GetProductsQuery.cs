using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_mvc.Dtos;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Queries
{
    public record GetProductsQuery :
        IRequest<IEnumerable<GetProductDto>>;

    public class GetProductsQueryHandler :
        IRequestHandler<GetProductsQuery, IEnumerable<GetProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public GetProductsQueryHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<GetProductDto>> Handle(
            GetProductsQuery request,
            CancellationToken token)
        {
            return await _context
                .Products
                .Where(p => p.DeletedAt == null)
                .ProjectTo<GetProductDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(token);
        }
    }
}
