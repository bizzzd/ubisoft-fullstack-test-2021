using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmeGames.Infrastructure.Persistance.EntityConfigurations
{
    class OwnershipEntityTypeConfiguration : IEntityTypeConfiguration<Ownership>
    {
        public void Configure(EntityTypeBuilder<Ownership> ownershipConfiguration)
        {
            ownershipConfiguration.HasKey(o => o.OwnershipId);
            ownershipConfiguration.HasOne(o => o.Game)
                .WithMany()
                .HasForeignKey(o => o.GameId);
        }
    }
}
