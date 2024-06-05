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
    public record CreateSaleCommand(IEnumerable<CreateSaleDto> CreateSaleDto, Guid UserId) : 
        IRequest<Result<Guid>>;

    public class CreateSaleCommandHandler :
        IRequestHandler<CreateSaleCommand, Result<Guid>>
    {
        private readonly StoreContext _dbContext;

        public CreateSaleCommandHandler(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            long totalAmount = 0;
            try
            {
                var products = await _dbContext.Products
                    .Where(x => request.CreateSaleDto.Select(x => x.ProductId).Contains(x.Id))
                    .ToArrayAsync(cancellationToken);

                foreach (var product in request.CreateSaleDto)
                {
                    totalAmount += product.Quantity * products.First(x => x.Id.Equals(product.ProductId)).Price;
                }

                var customerId = await _dbContext.Customers.Where(x => x.UserId == request.UserId)
                    .Select(x => x.Id)
                    .FirstAsync(cancellationToken);

                Order order = new()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    OrderNumber = SaleHelpers.GenerateOrderNumber(),
                    Status = (int)OrderEnums.Status.CREATED,
                    CreatedAt = DateTimeOffset.Now,
                    Sale = new Sale()
                    {
                        Id = Guid.NewGuid(),
                        Total = totalAmount,
                        CreatedAt = DateTimeOffset.Now,
                        ProductSales = new List<ProductSale>()
                    }
                };

                foreach (var item in request.CreateSaleDto)
                {
                    var product = products.Where(x => x.Id == item.ProductId).First();

                    if (product.Stock - item.Quantity <= 0)
                    {
                        return new Result<Guid>("Error, la cantidad de producto requerido supera la stock disponible.");
                    }

                    product.Stock -= item.Quantity;

                    order.Sale.ProductSales.Add(new ProductSale()
                    {
                        Product = product,
                        Sale = order.Sale,
                        Quantity = item.Quantity,
                        Subtotal = product.Price * item.Quantity,
                        UnitPrice = product.Price,
                    });
                }

                await _dbContext.Orders.AddAsync(order, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<Guid>(order.Sale.Id);
            }
            catch (Exception e) when (e is DbUpdateConcurrencyException cex) // Manejando error de concurrencia
            {
                foreach (var entry in cex.Entries)
                {
                    if (entry.Entity is Product)
                    {
                        using var transaction = _dbContext.Database.BeginTransaction();

                        var dbValues = await entry.GetDatabaseValuesAsync(cancellationToken);
                        var currValues = entry.CurrentValues;

                        var dbStock = (int)dbValues!["Stock"]!;

                        var requiredStock = request.CreateSaleDto.Sum(x => x.Quantity);
                        var diffStock = dbStock - requiredStock;

                        if (diffStock < 0)
                        {
                            return new Result<Guid>("Error al intentar realizar la compra. El stock disponible de uno de los productos es menor al deseado.");
                        }

                        dbValues["Stock"] = diffStock;
                        entry.OriginalValues.SetValues(dbValues);

                        await _dbContext.SaveChangesAsync(cancellationToken);
                        transaction.Commit();
                    }
                }
                return new Result<Guid>("Ha ocurrido un error inesperado al momento de realizar su compra. Por favor, recargue la página y si el error persiste, contactese con soporte.");
            }
            catch  // Delegar el manejo de la excepcion al GlobalErrorHandler
            {
                throw;
            }
        }
    }
}
