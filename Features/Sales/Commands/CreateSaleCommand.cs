using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Domain.Entities;

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
                OrderNumber = "1",
                Status = 0,
                CreatedAt = DateTimeOffset.Now
            };

            Sale sale = new()
            {
                Id = Guid.NewGuid(),
                
            };

            foreach (var item in request.CreateSaleDto)
            {
                var product = await _dbContext.Products
                    .AsNoTracking()
                    .Where(x => x.Id == sale.ProductId)
                    .FirstAsync(cancellationToken);
                try
                {
                    product.Stock -= sale.Quantity;
                }
                catch (DbUpdateConcurrencyException ex)
                {

                }
            }

            throw new NotImplementedException();
        }
    }
}
