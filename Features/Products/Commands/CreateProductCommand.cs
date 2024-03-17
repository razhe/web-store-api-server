using AutoMapper;
using MediatR;
using web_store_server.Domain.Dtos.Products;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_mvc.Features.Products.Commands
{
    public record CreateProductCommand(CreateProductDto CreateProductDto) 
        : IRequest;

    public class CreateProductCommandHandler : 
        IRequestHandler<CreateProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public CreateProductCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Handle(
            CreateProductCommand request, 
            CancellationToken token)
        {
            Product product = _mapper.Map<CreateProductDto, Product>(request.CreateProductDto);

            _context.Add(product);
            await _context.SaveChangesAsync(token);

            return;
        }
    }
}
