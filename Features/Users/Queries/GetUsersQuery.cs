using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using web_store_server.Domain.Communication;
using web_store_server.Domain.Dtos.Users;
using web_store_server.Persistence.Database;

namespace web_store_server.Features.Users.Queries
{
    public record GetUsersQuery : 
        IRequest<Result<IEnumerable<UserDto>>>;

    public class GetUsersQueryHanlder : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
    {
        private readonly StoreContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQueryHanlder(StoreContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserDto>>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _dbContext.Users
                    .Where(x => !x.IsDeleted)
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<UserDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
