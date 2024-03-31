using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using web_store_server.Common.Enums;
using web_store_server.Common.Helpers;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Sales;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Sales.Commands
{
    public record CreateSaleCommand(IEnumerable<CreateSaleDto> CreateSaleDto, Guid CustomerId) : 
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

            Order order = new()
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
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

                if (product.Stock - item.Quantity <= 0)
                {
                    return new Result<bool>("Error, la cantidad de producto requerido supera la stock disponible.");
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

            int attempts = 0;
            bool isSaved = false;

            while (attempts <= 2 || isSaved)
            {
                try
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    isSaved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Product)
                        {
                            /* Se podría considerar usar transacciones para garantizar que en el segundo intento
                            no se alteren los datos. Así combinamos ambas funcionalidades y nos beneficiamos de ambas ventajas
                            tambien podriamos omitir el bucle While. */

                            var databaseValues = await entry.GetDatabaseValuesAsync(cancellationToken);
                            var currentValues = (Product)entry.Entity;

                            if (databaseValues is null)
                            {
                                return new Result<bool>("Error al intentar generar una venta con ese producto, intentalo nuevamente. Si el error persiste comunicate con soporte.");
                            }

                            Guid databaseId = (Guid)databaseValues["id"]!;
                            int databaseStock = (int)databaseValues["stock"]!;

                            var requiredStock = request.CreateSaleDto.Where(x => x.ProductId == databaseId).Select(x => x.Quantity).First();

                            int stockDiff = databaseStock - requiredStock;

                            if (stockDiff < 0)
                            {
                                return new Result<bool>("Error al intentar realizar la compra. El stock de uno de los productos es menor al deseado.");
                            }
                            else
                            {
                                currentValues.Stock = stockDiff;
                                entry.CurrentValues.SetValues(currentValues);

                                await _dbContext.SaveChangesAsync(cancellationToken);
                            }
                        }
                    }

                    isSaved = true;
                }
                attempts++;
            }

            if (!isSaved) 
            {
                return new Result<bool>("Error al procesar la compra, vacíe el carrito e inténtelo nuevamente.");
            }

            return new Result<bool>(true);
        }
    }
}
