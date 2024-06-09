using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Admin;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Dashboard.Queries
{
    public record GetDashboardQuery() : 
        IRequest<Result<DashboardDto>>;

    public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, Result<DashboardDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public GetDashboardQueryHandler(IMapper mapper, StoreContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Result<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            DashboardDto dashboard = new();

            try
            {
                dashboard.TotalSales = await TotalSalesLastWeek();
                dashboard.TotalProfit = await TotalProfitLastWeek();
                dashboard.TotalProducts = await TotalProducts();
                dashboard.LastWeekSales = await SalesLastWeek();

                return new Result<DashboardDto>(dashboard);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Recibe la tabla de ventas y retorna un rango de ventas de acuerdo a una fecha
        /// </summary>
        /// <param name="salesTable"></param>
        /// <param name="numberDays"></param>
        /// <returns></returns>
        private async Task<IQueryable<Sale>> ReturnSales(IQueryable<Sale> salesTable, int numberDays)
        {
            try
            {
                DateTimeOffset lastSaleDate = await salesTable.OrderByDescending(x => x.CreatedAt)
                .Select(x => x.CreatedAt)
                .FirstAsync();

                lastSaleDate = lastSaleDate.AddDays(numberDays);
                return salesTable.Where(x => x.CreatedAt >= lastSaleDate);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna el total de ventas de la ultima semana
        /// </summary>
        /// <returns></returns>
        private async Task<int> TotalSalesLastWeek()
        {
            int totalSales = default;

            try
            {
                IQueryable<Sale> saleQuery = _dbContext.Sales;
                if (saleQuery.Any())
                {
                    var salesTable = await ReturnSales(saleQuery, -7);
                    totalSales = await salesTable.CountAsync();
                }

                return totalSales;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna el total de ganancias de la ultima semana
        /// </summary>
        /// <returns></returns>
        private async Task<string> TotalProfitLastWeek()
        {
            long result = default;

            try
            {
                IQueryable<Sale> saleQuery = _dbContext.Sales;
                if (saleQuery.Any())
                {
                    var salesTable = await ReturnSales(saleQuery, -7);
                    result = await salesTable.Select(x => x.Total).SumAsync();
                }

                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna el total de productos registrados
        /// </summary>
        /// <returns></returns>
        private async Task<int> TotalProducts()
        {
            try
            {
                IQueryable<Product> productQuery = _dbContext.Products;

                int total = await productQuery.CountAsync();
                return total;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna las ventas de la ultima semana
        /// </summary>
        /// <returns></returns>
        private async Task<List<WeekSalesDto>> SalesLastWeek() 
        {
            List<WeekSalesDto> result = new ();
            IQueryable<Sale> saleQuery = _dbContext.Sales;

            try
            {
                if (saleQuery.Any())
                {
                    var salesTable = await ReturnSales(saleQuery, -7);

                    result = await salesTable.GroupBy(x => x.CreatedAt.ToString("dd/MM/yyyy"))
                        .OrderBy(group => group.Key)
                        .Select(group => new WeekSalesDto
                        {
                            Date = group.Key,
                            Total = group.Count()
                        })
                        .ToListAsync();

                }
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
