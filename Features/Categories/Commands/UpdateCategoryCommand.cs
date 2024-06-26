﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Categories;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Categories.Commands
{
    public record UpdateCategoryCommand(CreateUpdateCategoryDto CategoryDto, int CategoryId) : 
        IRequest<Result<CategoryDto>>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public UpdateCategoryCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productCategory = await _dbContext
                    .ProductCategories
                    .Where(x => x.Id == request.CategoryId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (productCategory is null)
                {
                    return new Result<CategoryDto>("No se ha encontrado una categoría con ese identificador.");
                }

                request.CategoryDto.MapToModel(productCategory);
                await _dbContext.SaveChangesAsync(cancellationToken);

                CategoryDto categoryUpdated = _mapper.Map<CategoryDto>(productCategory);

                return new Result<CategoryDto>(categoryUpdated);
            }
            catch
            {
                throw;
            }
        }
    }
}
