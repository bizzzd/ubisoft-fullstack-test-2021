using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmeGames.Infrastructure.Persistance.EntityConfigurations
{
    class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> gameConfiguration)
        {
            gameConfiguration.HasKey(g => g.GameId);

            gameConfiguration.Property(g => g.Name)
                .HasMaxLength(250)
                .IsRequired();

            gameConfiguration.Property(g => g.Thumbnail)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
