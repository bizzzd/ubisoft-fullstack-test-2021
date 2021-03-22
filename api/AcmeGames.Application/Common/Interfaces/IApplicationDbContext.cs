using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Ownership> Ownership { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameKey> Keys { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
