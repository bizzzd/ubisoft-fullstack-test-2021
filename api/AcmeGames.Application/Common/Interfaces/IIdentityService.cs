using AcmeGames.Application.Commands.AuthenticateUser;
using AcmeGames.Application.Common.Models;
using System.Threading.Tasks;

namespace AcmeGames.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<AuthenticationData>> Authenticate(string email, string password);
    }
}
