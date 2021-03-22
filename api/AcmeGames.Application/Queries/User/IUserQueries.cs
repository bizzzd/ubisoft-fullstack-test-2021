using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeGames.Application.Queries.User
{
    public interface IUserQueries
    {
        Task<bool> IsEmailFreeToUse(string email);

        Task<UserPersonalInfoDto> GetCurrentUserPersonalInfo();

        Task<IEnumerable<Domain.User>> GetUsers();
    }
}
