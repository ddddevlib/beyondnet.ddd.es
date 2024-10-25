using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.Models;
using BeyondNet.Ddd.Es.MongoDb.Tables;
using BeyondNet.Ddd.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace BeyondNet.Ddd.Es.MongoDb
{
    public class MongoDbEventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventDataRecordTable> mongoCollection;

        public MongoDbEventStoreRepository(IOptions<MongoDbConfig> config)
        {
            if (config is null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var mongoClient = new MongoClient(config.Value.ConnectionString);
            var database = mongoClient.GetDatabase(config.Value.Database);
            
            mongoCollection = database.GetCollection<EventDataRecordTable>(config.Value.Collection);

        }

        public async Task<ICollection<EventDataRecord>> Load(Guid aggregateId)
        {
            if (aggregateId == Guid.Empty)
                throw new ArgumentNullException(nameof(aggregateId));

            var filter = Builders<EventDataRecordTable>.Filter.Eq(x => x.AggregateId, aggregateId);

            var result = await mongoCollection.Find(filter).ToListAsync().ConfigureAwait(false);

            return result.Select(TransformTableToModel).ToList();
        }

        public async Task SaveAsync(EventDataRecord @event)
        {
            await mongoCollection.InsertOneAsync(TransformModelToTable(@event)).ConfigureAwait(false);
        }

        public EventDataRecordTable TransformModelToTable(EventDataRecord @event)
        {
            return new EventDataRecordTable
            {
                Id = @event.Id,
                AggregateId = @event.AggregateId,
                AggregateType = @event.AggregateType,
                Version = @event.Version,
                EventType = @event.EventType,
                EventData = JsonSerializer.Serialize<IDomainEvent>(@event.EventData),
                AssemblyQualifyName = @event.AssemblyQualifyName,
                CreatedAt = @event.CreatedAt
            };
        }

        public EventDataRecord TransformTableToModel(EventDataRecordTable table)
        {
            return new EventDataRecord
            {
                Id = table.Id,
                AggregateId = table.AggregateId,
                AggregateType = table.AggregateType,
                Version = table.Version,
                EventType = table.EventType,
                EventData = JsonSerializer.Deserialize<IDomainEvent>(table.EventData)!,
                AssemblyQualifyName = table.AssemblyQualifyName,
                CreatedAt = table.CreatedAt
            };
        }
    }
}
