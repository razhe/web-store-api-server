using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid ProductId) :
        IRequest<Result<GetProductDto?>>;

    public class GetProductByIdQueryHandler :
        IRequestHandler<GetProductByIdQuery, Result<GetProductDto?>>
    {
        private readonly StoreContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(StoreContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Result<GetProductDto?>> Handle(
            GetProductByIdQuery request,
            CancellationToken token)
        {
            try
            {
                var response = await _dbContext
                    .Products
                    .Where(x => x.Id == request.ProductId)
                    .ProjectTo<GetProductDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(token);

                return new Result<GetProductDto?>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
