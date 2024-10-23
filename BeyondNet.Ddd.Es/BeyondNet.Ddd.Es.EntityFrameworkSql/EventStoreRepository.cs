using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.Models;
using Microsoft.EntityFrameworkCore;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EsDbContext esDbContext;

        public EventStoreRepository(EsDbContext esDbContext)
        {
            this.esDbContext = esDbContext ?? throw new ArgumentNullException(nameof(esDbContext));
        }

        public async Task<ICollection<EventDataRecord>> Load(string aggregateId)
        {
            var eventRecordsData = await esDbContext.EventDataRecords
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Version)
                .ToListAsync();

            return eventRecordsData;
        }

        public async Task Save(EventDataRecord eventData)
        {
            if (eventData is null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            await esDbContext.EventDataRecords.AddAsync(eventData);
            await esDbContext.SaveChangesAsync();
        }
    }
}
