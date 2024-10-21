using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.Models;
using BeyondNet.Ddd.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BeyondNet.Ddd.Es.Impl
{
    public class EventStore<TAggregateRoot> : IEventStore<TAggregateRoot>
            where TAggregateRoot : AggregateRoot<TAggregateRoot, IProps>
            
    {
        private readonly IEventStoreRepository eventStoreRepository;
        private readonly ILogger<EventStore<TAggregateRoot>> logger;

        public EventStore(IEventStoreRepository eventStoreRepository, ILogger<EventStore<TAggregateRoot>> logger)
        {
            this.eventStoreRepository = eventStoreRepository ?? throw new ArgumentNullException(nameof(eventStoreRepository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TAggregateRoot> Load(TAggregateRoot aggregate)
        {
            var eventRecordsData = await eventStoreRepository.Load(aggregate.Id.GetValue());

            var domainEvents = eventRecordsData.Select(e =>
            {
                var assemblyQualifyName = e.AssemblyQualifyName;
                var eventType = Type.GetType(assemblyQualifyName);
                var eventData = JsonSerializer.Deserialize(e.EventData, eventType!);
                return eventData as IDomainEvent;
            }).ToList().AsReadOnly();


            if (aggregate is null)
            {
                logger.LogError("Aggregate cannot be null");
                throw new ArgumentNullException(nameof(aggregate));
            }

            if (!domainEvents.Any())
            {
                logger.LogInformation("No events to load");
                return aggregate;
            }

            aggregate.LoadDomainEvents(domainEvents!);

            return aggregate;
        }
        public async Task Save(TAggregateRoot aggregate)
        {
            if (aggregate is null)
            {
                logger.LogError("Aggregate cannot be null");
                throw new ArgumentNullException(nameof(aggregate));
            }

            var aggregateId = aggregate.Id.GetValue();

            var domainEvents = aggregate.GetDomainEvents();

            if (domainEvents.Count == 0)
            {
                logger.LogInformation("No changes to save");
                return;
            }

            var eventDataRecords = ConvertAggregateToEventRecords(aggregate, aggregateId, domainEvents);

            foreach (var eventDataRecord in eventDataRecords)
            {
                await eventStoreRepository.Save(eventDataRecord);
            }

            aggregate.ClearDomainEvents();
        }

        private List<EventDataRecord> ConvertAggregateToEventRecords(TAggregateRoot aggregate, string aggregateId, IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            var eventDataRecords = new List<EventDataRecord>();

            try
            {
                foreach (var domainEvent in domainEvents)
                {
                    var recordData = new EventDataRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        AggregateId = aggregateId,
                        AssemblyQualifyName = domainEvent.GetType().AssemblyQualifiedName!,
                        EventData = JsonSerializer.Serialize(domainEvent),
                        EventName = domainEvent.GetType().Name,
                        CreatedAt = domainEvent.CreatedAt
                    };

                    eventDataRecords.Add(recordData);
                }
            }
            catch (Exception err)
            {
                logger.LogError($"Error creating EventDataRecord. Message:{err.Message}, Stack:{err.StackTrace}");
                throw;
            }

            return eventDataRecords;
        }
    }
}
