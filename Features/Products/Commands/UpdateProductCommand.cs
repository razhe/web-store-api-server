using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_mvc.Dtos;
using web_store_server.Domain.Models.Entities;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Commands
{
    public record UpdateProductCommand(UpdateProductDto UpdateProductDto) : IRequest;

    public class UpdateProductCommandHandler :
        IRequestHandler<UpdateProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public UpdateProductCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(
            UpdateProductCommand request, 
            CancellationToken token)
        {
            Product productUpdated = _mapper.Map<UpdateProductDto, Product>(request.UpdateProductDto);

            var product = await _context
                .Products
                .Where(x => x.Id == request.UpdateProductDto.Id)
                .FirstAsync(token);

            _context.Entry(product).CurrentValues.SetValues(productUpdated);

            await _context.SaveChangesAsync(token);

            return;
        }
    }
}
