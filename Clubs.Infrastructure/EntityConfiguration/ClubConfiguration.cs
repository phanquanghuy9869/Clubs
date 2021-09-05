using System;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clubs.Infrastructure.EntityConfiguration
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public ClubConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<Club> clubBuilder)
        {
            clubBuilder.HasKey(c => c.Id);
            clubBuilder.Ignore(c => c.DomainEvents);
            clubBuilder.Property(v => v.Id).ValueGeneratedOnAdd();
            clubBuilder.HasIndex(c => c.ClubId).IsUnique();
            clubBuilder.Property(c => c.ClubId).HasMaxLength(200);
        }
    }
}
