using AcmeGames.Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeGames.Application.Queries.User
{
    public class UserQueries : IUserQueries
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UserQueries(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, 
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<UserPersonalInfoDto> GetCurrentUserPersonalInfo()
        {
            var userAccountId = _currentUserService.UserId;

            return await _applicationDbContext.Users
                .Where(u => u.UserAccountId == userAccountId)
                .ProjectTo<UserPersonalInfoDto>((_mapper.ConfigurationProvider))
                .SingleAsync();
        }

        public async Task<bool> IsEmailFreeToUse(string email)
        {
            var userAccountId = _currentUserService.UserId;

            return !(await _applicationDbContext.Users
                .AnyAsync(u => u.EmailAddress == email && u.UserAccountId != userAccountId));
        }
        public async Task<IEnumerable<Domain.User>> GetUsers()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }
    }
}
