using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Sales.Queries
{
    public record GetHistoryQuery(string SearchTerm, string SaleId, DateTimeOffset StartDate, DateTimeOffset EndDate) :
        IRequest<Result<IEnumerable<GetSaleDto>>>;

    public class GetHistoryQueryHandler : IRequestHandler<GetHistoryQuery, Result<IEnumerable<GetSaleDto>>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public GetHistoryQueryHandler(IMapper mapper, StoreContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<GetSaleDto>>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Sale> saleQuery = _dbContext.Sales;
            IEnumerable<Sale> resultList;

            try
            {
                if (request.SearchTerm.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    resultList = await saleQuery.Where(x =>
                        x.CreatedAt >= request.StartDate &&
                        x.CreatedAt <= request.EndDate)
                    .Include(x => x.Order)
                    .Include(x => x.ProductSales)
                    .ThenInclude(x => x.Product)
                    .ToArrayAsync(cancellationToken);
                }
                else
                {
                    resultList = await saleQuery.Where(x =>
                        x.Order.OrderNumber.Equals(request.SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .Include(x => x.Order)
                    .Include(x => x.ProductSales)
                    .ThenInclude(x => x.Product)
                    .ToArrayAsync(cancellationToken);
                }

                return new Result<IEnumerable<GetSaleDto>>(_mapper.Map<IEnumerable<GetSaleDto>>(resultList));
            }
            catch
            {
                throw;
            }
        }
    }
}
