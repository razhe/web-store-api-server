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
        IRequest<Result<IEnumerable<GetUserDto>>>;

    public class GetUsersQueryHanlder : IRequestHandler<GetUsersQuery, Result<IEnumerable<GetUserDto>>>
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHanlder(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetUserDto>>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _context.Users
                    .Where(x => !x.IsDeleted)
                    .ProjectTo<GetUserDto>(_mapper.ConfigurationProvider).ToArrayAsync(cancellationToken);

                return new Result<IEnumerable<GetUserDto>>(response);
            }
            catch
            {
                throw;
            }
        }
    }
}
