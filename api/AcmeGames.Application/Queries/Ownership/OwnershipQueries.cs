using AcmeGames.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeGames.Application.Queries.Ownership
{
    public class OwnershipQueries : IOwnershipQueries
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;

        public OwnershipQueries(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, 
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MyGameDto>> GetMyGames()
        {
            var currentUserId = _currentUserService.UserId;

            return await _applicationDbContext.Ownership
                .Where(o => o.UserAccountId == currentUserId && o.State == Domain.OwnershipState.Owned)
                .OrderByDescending(o => o.RegisteredDate)
                .ProjectTo<MyGameDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
