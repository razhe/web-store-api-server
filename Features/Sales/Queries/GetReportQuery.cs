using MediatR;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Dtos.Sales;

namespace web_store_server.Features.Sales.Queries
{
    public record GetReportQuery(DateTimeOffset StartDate, DateTimeOffset EndDate) :
        IRequest<Result<IEnumerable<ReportDto>>>;

    public class GetReportQueryHandler : IRequestHandler<GetReportQuery, Result<IEnumerable<ReportDto>>>
    { 
        public async Task<Result<IEnumerable<ReportDto>>> Handle(GetReportQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
