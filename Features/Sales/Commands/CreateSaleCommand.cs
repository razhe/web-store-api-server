using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Common.Helpers;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Sales.Commands
{
    public record CreateSaleCommand(IEnumerable<CreateSaleDto> CreateSaleDto, Guid customerId) : 
        IRequest<Result<bool>>;

    public class CreateSaleCommandHandler :
        IRequestHandler<CreateSaleCommand, Result<bool>>
    {
        private readonly StoreContext _dbContext;

        public CreateSaleCommandHandler(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            Order order = new()
            {
                Id = Guid.NewGuid(),
                CustomerId = request.customerId,
                OrderNumber = SaleHelpers.GenerateOrderNumber(),
                Status = 0,
                CreatedAt = DateTimeOffset.Now
            };

            Sale sale = new()
            {
                Id = Guid.NewGuid(),
                Order = order,
            };

            foreach (var item in request.CreateSaleDto)
            {
                var product = await _dbContext.Products
                    .AsNoTracking()
                    .Where(x => x.Id == item.ProductId)
                    .FirstAsync(cancellationToken);
                try
                {
                    product.Stock -= item.Quantity;
                }
                catch (DbUpdateConcurrencyException ex)
                {

                }
            }

            throw new NotImplementedException();
        }
    }
}
