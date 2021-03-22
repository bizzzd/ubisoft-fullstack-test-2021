using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeGames.Application.Queries.Ownership
{
    public interface IOwnershipQueries
    {
        Task<IEnumerable<MyGameDto>> GetMyGames();
    }
}
