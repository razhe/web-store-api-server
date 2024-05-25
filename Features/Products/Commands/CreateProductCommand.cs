﻿using AutoMapper;
using MediatR;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Commands
{
    public record CreateProductCommand(CreateUpdateProductDto Product) 
        : IRequest;

    public class CreateProductCommandHandler : 
        IRequestHandler<CreateProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public CreateProductCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task Handle(
            CreateProductCommand request, 
            CancellationToken token)
        {
            try
            {
                Product product = _mapper.Map<CreateUpdateProductDto, Product>(request.Product);

                _dbContext.Add(product);
                await _dbContext.SaveChangesAsync(token);

                return;
            }
            catch
            {
                throw;
            }
        }
    }
}
