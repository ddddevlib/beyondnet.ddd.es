using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BeyondNet.Ddd.Es.Impl
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository eventStoreRepository;
        private readonly ILogger<EventStore> logger;

        public EventStore(IEventStoreRepository eventStoreRepository, ILogger<EventStore> logger)
        {
            this.eventStoreRepository = eventStoreRepository ?? throw new ArgumentNullException(nameof(eventStoreRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Save(AggregateRoot aggregate)
        {
            if (aggregate == null)
            {
                logger.LogError("Aggregate is null");
                throw new ArgumentNullException(nameof(aggregate));
            }

            var aggregateName = aggregate.GetType().Name;
            var aggregateId = $"{aggregateName}-{aggregate.Id}";

            var allChanges = aggregate.GetDomainEvents();

            if (allChanges.Count == 0)
            {
                logger.LogInformation("No changes to save");
                return;
            }

            var eventDataRecords = allChanges.Select(@event => new EventDataRecord
            {
                Id = Guid.NewGuid().ToString(),
                AggregateId = aggregateId,
                AssemblyQualifyName = @event.GetType().AssemblyQualifiedName!,
                EventData = JsonSerializer.Serialize(@event),
                DataObject = JsonSerializer.Serialize(aggregate),
                EventName = @event.GetType().Name,
                CreatedAt = @event.CreatedAt
            });

            if (!eventDataRecords.Any())
            {
                logger.LogError("No events to save");
                return;
            }

            foreach (var eventDataRecord in eventDataRecords)
            {
                await eventStoreRepository.Save(eventDataRecord);
            }

            aggregate.ClearDomainEvents();
        }
    }
}
