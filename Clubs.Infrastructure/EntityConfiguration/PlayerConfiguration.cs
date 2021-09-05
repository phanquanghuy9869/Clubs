using System;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clubs.Infrastructure.EntityConfiguration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public PlayerConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Player> playerBuilder)
        {
            playerBuilder.HasKey(p => p.Id);
            playerBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
            playerBuilder.Ignore(p => p.DomainEvents);
            playerBuilder.HasOne<Club>().WithMany(c => c.Players).HasForeignKey("ClubId").IsRequired(true);
        }
    }
}
