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

        public async Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
            {
                logger.LogError("AggregateId cannot be empty");
                throw new ArgumentException("AggregateId cannot be empty", nameof(aggregateId));
            }

            var eventRecordsData = await eventStoreRepository.Load(aggregateId);

            var eventStream = await eventStoreRepository.Load(aggregateId);

            if (eventStream == null || !eventStream.Any())
            {
                logger.LogInformation("No events found");
                throw new InvalidOperationException("No events found");
            }

            return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
        }

        public async Task SaveAsync(Guid aggregateId, string aggregateType, ICollection<IDomainEvent> events, int expectedVersion)
        {
            var eventStream = await eventStoreRepository.Load(aggregateId);

            if (eventStream == null || !eventStream.Any())
            {
                logger.LogInformation("No events found");
                throw new InvalidOperationException("No events found");
            }

            if (expectedVersion != -1 && eventStream.Last().Version != expectedVersion)
            {
                throw new Exception("Concurrency exception");
            }

            var version = expectedVersion;

            foreach (var @event in events)
            {
                version++;
                
                @event.Version = version;
                
                var eventType = @event.GetType().Name;

                var eventModel = new EventDataRecord
                {
                    AggregateId = aggregateId,
                    AggregateType = aggregateType,
                    Version = version,
                    EventType = eventType,
                    EventData = (DomainEvent)@event,
                };

                await eventStoreRepository.SaveAsync(eventModel);
            }
        }
    }
}
