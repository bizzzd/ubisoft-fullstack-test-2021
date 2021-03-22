using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmeGames.Infrastructure.Persistance.EntityConfigurations
{
    class GameKeyEntityTypeConfiguration : IEntityTypeConfiguration<GameKey>
    {
        public void Configure(EntityTypeBuilder<GameKey> gameKeyConfiguration)
        {
            gameKeyConfiguration.HasKey(o => o.Key);
            gameKeyConfiguration.HasOne(o => o.Game)
                .WithMany()
                .HasForeignKey(o => o.GameId);
        }
    }
}
