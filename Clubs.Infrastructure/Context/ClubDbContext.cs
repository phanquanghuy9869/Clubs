using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clubs.Domain.AggregateModels.ClubAggregate;
using Clubs.Domain.SeedWorks;
using Clubs.Infrastructure.EntityConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clubs.Infrastructure.Context
{
    public class ClubDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }

        public ClubDbContext([NotNullAttribute] DbContextOptions<ClubDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await this.DispatchDomainEventAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClubConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
        }

        private async Task DispatchDomainEventAsync()
        {
            var domainEntities = this.ChangeTracker
               .Entries<Entity>()
               .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}
