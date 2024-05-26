using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Sales.Queries
{
    public record GetReportQuery(DateTimeOffset StartDate, DateTimeOffset EndDate) :
        IRequest<Result<IEnumerable<ReportDto>>>;

    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, Result<IEnumerable<ReportDto>>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public GetReportQueryHandler(IMapper mapper, StoreContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<ReportDto>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductSale> productSaleQuery = _dbContext.ProductSales;
            IEnumerable<ProductSale> resultList;

            try
            {
                resultList = await productSaleQuery
                    .Include(x => x.Product)
                    .Include(x => x.Sale)
                    .ThenInclude(x => x.Order)
                    .Where(x => 
                        x.Sale.CreatedAt >= request.StartDate &&
                        x.Sale.CreatedAt <= request.EndDate
                    ).ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<ReportDto>>(_mapper.Map<IEnumerable<ReportDto>>(resultList));
            }
            catch
            {
                throw;
            }
        }
    }
}
