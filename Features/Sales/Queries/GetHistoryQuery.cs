using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Sales.Queries
{
    public record GetHistoryQuery(SalesHistoryQueryParams QueryParams) :
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
                if (request.QueryParams.SearchTerm.Equals("date", StringComparison.OrdinalIgnoreCase))
                {
                    if (request.QueryParams.StartDate is null || request.QueryParams.EndDate is null)
                    {
                        return new Result<IEnumerable<GetSaleDto>>("Si el filtro es de tipo Fecha debes incluir los campos 'StartDate' y 'EndDate'.");
                    }

                    resultList = await saleQuery.Where(x =>
                        x.CreatedAt >= request.QueryParams.StartDate &&
                        x.CreatedAt <= request.QueryParams.EndDate)
                    .Include(x => x.Order)
                    .Include(x => x.ProductSales)
                    .ThenInclude(x => x.Product)
                    .ToArrayAsync(cancellationToken);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(request.QueryParams.OrderNumber))
                    {
                        return new Result<IEnumerable<GetSaleDto>>("Si el filtro es de tipo Numero de orden debes incluir el campo 'OrderNumber'.");
                    }

                    resultList = await saleQuery.Where(x =>
                        x.Order.OrderNumber.ToUpper().Equals(request.QueryParams.SearchTerm.ToUpper()))
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
