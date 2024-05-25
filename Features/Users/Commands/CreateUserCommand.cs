using AutoMapper;
using MediatR;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Commands
{
    public record CreateUserCommand(CreateUpdateUserDto UserDto) : 
        IRequest<Result<CreateUpdateUserDto>>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUpdateUserDto>>
    {
        private readonly StoreContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(StoreContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Result<CreateUpdateUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = _mapper.Map<CreateUpdateUserDto, User>(request.UserDto);
                await _dbContext.AddAsync(user, cancellationToken);

                return new Result<CreateUpdateUserDto>(request.UserDto);
            }
            catch
            {
                throw;
            }
        }
    }
}
