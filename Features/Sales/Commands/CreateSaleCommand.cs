using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Common.Enums;
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
            var products = await _dbContext.Products
                .Where(x => request.CreateSaleDto.Select(x => x.ProductId).Contains(x.Id))
                .ToArrayAsync(cancellationToken);

            try
            {
                Order order = new()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = request.customerId,
                    OrderNumber = SaleHelpers.GenerateOrderNumber(),
                    Status = (int)OrderEnums.Status.CREATED,
                    CreatedAt = DateTimeOffset.Now,
                    Sale = new Sale()
                    {
                        Id = Guid.NewGuid(),
                        Total = products.Select(x => x.Price).Sum(),
                        CreatedAt = DateTimeOffset.Now,
                        ProductSales = new List<ProductSale>()
                    }
                };

                foreach (var item in request.CreateSaleDto)
                {
                    var product = products.Where(x => x.Id == item.ProductId).First();
                    order.Sale.ProductSales.Add(new ProductSale()
                    {
                        Product = product,
                        Sale = order.Sale,
                        Quantity = item.Quantity,
                        Subtotal = product.Price * item.Quantity,
                        UnitPrice = product.Price,
                    });

                    product.Stock -= item.Quantity;
                }

                await _dbContext.Orders.AddAsync(order, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {

            }

            return new Result<bool>(true);
        }
    }
}
