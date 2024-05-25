using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Commands
{
    public record UpdateUserCommand(CreateUpdateUserDto UserDto, Guid UserId) : IRequest<Result<CreateUpdateUserDto>>;

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<CreateUpdateUserDto>>
    {
        private readonly IMapper _mapper;
        private readonly StoreContext _dbContext;

        public UpdateUserCommandHandler(IMapper mapper, StoreContext context)
        {
            _mapper = mapper;
            _dbContext = context;
        }

        public async Task<Result<CreateUpdateUserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext
                    .Users
                    .Where(x => x.Id == request.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user is null)
                {
                    return new Result<CreateUpdateUserDto>("No se ha encontrado un usuario con ese identificador.");
                }

                request.UserDto.MapToModel(user);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<CreateUpdateUserDto>(request.UserDto);
            }
            catch
            {
                throw;
            }
        }
    }
}
