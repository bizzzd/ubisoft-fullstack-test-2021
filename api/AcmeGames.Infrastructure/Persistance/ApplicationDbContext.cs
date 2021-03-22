using AcmeGames.Application.Common.Interfaces;
using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AcmeGames.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Ownership> Ownership { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameKey> Keys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedSampleData();
        }
    }
}
