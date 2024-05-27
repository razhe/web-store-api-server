using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Common.Extensions;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Domain.Entities;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Commands
{
    public record CreateUserCommand(CreateUpdateUserDto UserDto) : 
        IRequest<Result<Guid>>;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly StoreContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(StoreContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool userExists = await _dbContext.Users
                    .AnyAsync(x => x.Email.Trim().ToUpper() == request.UserDto.Email.Trim().ToUpper(), 
                    cancellationToken);
                
                if (userExists)
                {
                    return new Result<Guid>("Ya existe un usuario con ese correo.");
                }

                User user = _mapper.Map<CreateUpdateUserDto, User>(request.UserDto);
                user.EncryptPassword(user.Password);

                await _dbContext.AddAsync(user, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Result<Guid>(user.Id);
            }
            catch
            {
                throw;
            }
        }
    }
}
