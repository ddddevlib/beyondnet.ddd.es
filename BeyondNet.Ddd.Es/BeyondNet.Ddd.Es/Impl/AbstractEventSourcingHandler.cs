using BeyondNet.Cqrs.Commands.Interfaces;


namespace BeyondNet.Ddd.Es.Impl
{
    public abstract class AbstractEventSourcingHandler<T> : IEventSourcingHandler<T>
                                               where T : IAggregateRoot
    {
        private readonly IEventStore eventStore;

        public AbstractEventSourcingHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public async Task<T> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = (T?)Activator.CreateInstance(typeof(T), true);

            if (aggregate == null)
            {
                throw new InvalidOperationException($"Could not create an instance of {typeof(T)}.");
            }

            var events = await eventStore.GetEventsAsync(aggregateId);

            if (events == null || !events.Any()) return aggregate;

            aggregate.DomainEvents.ReplayEvents(events);

            var latestVersion = events.Select(x => x.Version).Max();

            aggregate.Version = latestVersion;

            return aggregate;
        }

        public async Task SaveAsync(T aggregateRoot)
        {
            if (aggregateRoot == null)
            {
                throw new ArgumentNullException(nameof(aggregateRoot));
            }

            await eventStore.SaveAsync(aggregateRoot.Id.GetValue(), aggregateRoot.GetType().AssemblyQualifiedName!, aggregateRoot.DomainEvents.GetUncommittedChanges().ToList(), aggregateRoot.DomainEvents.Version);
            
            aggregateRoot.DomainEvents.MarkChangesAsCommitted();
        }
    }
}
