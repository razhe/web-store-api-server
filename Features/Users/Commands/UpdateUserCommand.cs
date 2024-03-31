using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Commands
{
    public record UpdateUserCommand(UserDto UserDto, Guid UserId) : IRequest<Result<UserDto>>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public UpdateUserCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context
                    .Users
                    .Where(x => x.Id == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user is null)
                {
                    return new Result<UserDto>("No se ha encontrado un usuario con ese identificador.");
                }

                request.UserDto.MapToModel(user);
                await _context.SaveChangesAsync(cancellationToken);

                return new Result<UserDto>(request.UserDto);
            }
            catch
            {
                throw;
            }
        }
    }
}
