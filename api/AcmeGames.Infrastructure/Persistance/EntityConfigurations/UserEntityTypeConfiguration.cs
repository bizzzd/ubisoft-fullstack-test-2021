using AcmeGames.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmeGames.Infrastructure.Persistance.EntityConfigurations
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> userConfiguration)
        {
            userConfiguration.HasKey(u => u.UserAccountId);

            userConfiguration.Property(u => u.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            userConfiguration.Property(u => u.LastName)
                .HasMaxLength(100)
                .IsRequired();

            userConfiguration.Property(u => u.EmailAddress)
                .HasMaxLength(255)
                .IsRequired();

            userConfiguration.HasIndex(u => u.EmailAddress)
                .IsUnique();

            userConfiguration.HasMany(u => u.Ownership)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserAccountId);

            userConfiguration.Ignore(u => u.FullName);
        }
    }
}
