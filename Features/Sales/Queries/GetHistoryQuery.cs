using MediatR;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;

namespace web_store_server.Features.Sales.Queries
{
    public record GetHistoryQuery(string SearchTerm, string SaleId, DateTimeOffset StartDate, DateTimeOffset EndDate) :
        IRequest<Result<IEnumerable<GetSaleDto>>>;

    public class GetHistoryQueryHandler : IRequestHandler<GetHistoryQuery, Result<IEnumerable<GetSaleDto>>>
    { 
        public async Task<Result<IEnumerable<GetSaleDto>>> Handle(GetHistoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
