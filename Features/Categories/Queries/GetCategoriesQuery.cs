﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Queries
{
    public record GetBrandsQuery : IRequest<Result<IEnumerable<CategoryDto>>>;

    public class GetBrandsQueryHandler : IRequestHandler<GetBrandsQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public GetBrandsQueryHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _dbContext
                    .ProductCategories
                    .Where(p => !p.IsDeleted)
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<CategoryDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
