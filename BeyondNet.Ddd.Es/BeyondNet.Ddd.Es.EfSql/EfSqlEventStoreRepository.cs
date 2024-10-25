using BeyondNet.Ddd.Es.EntityFrameworkSql.Tables;
using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.Models;
using BeyondNet.Ddd.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql
{
    public class EfSqlEventStoreRepository : IEventStoreRepository
    {
        private readonly EsDbContext esDbContext;

        public EfSqlEventStoreRepository(EsDbContext esDbContext)
        {
            this.esDbContext = esDbContext ?? throw new ArgumentNullException(nameof(esDbContext));
        }

        public async Task<ICollection<EventDataRecord>> Load(Guid aggregateId)
        {
            var result = await esDbContext.EventDataRecords
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Version)
                .ToListAsync();

           return result.Select(TransformTableToModel).ToList();
        }

        public async Task SaveAsync(EventDataRecord @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }
            
            await esDbContext.EventDataRecords.AddAsync(TransformModelToTable(@event));
            await esDbContext.SaveChangesAsync();
        }

        private EventDataRecord TransformTableToModel(EventDataRecordTable table)
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

        private EventDataRecordTable TransformModelToTable(EventDataRecord @event)
        {
            return new EventDataRecordTable
            {
                Id = @event.Id,
                AggregateId = @event.AggregateId,
                AggregateType = @event.AggregateType,
                Version = @event.Version,
                EventType = @event.EventType,
                EventData = JsonSerializer.Serialize(@event.EventData),
                AssemblyQualifyName = @event.AssemblyQualifyName,
                CreatedAt = @event.CreatedAt
            };
        }
    }
}
