﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Queries
{
    public record GetCategoriesQuery : IRequest<Result<IEnumerable<GetProductCategoryDto>>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Result<IEnumerable<GetProductCategoryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public GetCategoriesQueryHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<IEnumerable<GetProductCategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _context
                    .ProductCategories
                    .Where(p => !p.IsDeleted)
                    .ProjectTo<GetProductCategoryDto>(_mapper.ConfigurationProvider).ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<GetProductCategoryDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
