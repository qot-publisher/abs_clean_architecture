using Abs.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Abs.Infrastructure
{
    internal class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
            : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
