using BeyondNet.Ddd.Es.Interfaces;
using BeyondNet.Ddd.Es.Models;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql
{
    public class EventStoreRepository : IEventStoreRepository
    {
        public async Task<ICollection<EventDataRecord>> Load(string aggregateId)
        {
            throw new NotImplementedException();
        }

        public async Task Save(EventDataRecord eventData)
        {
            throw new NotImplementedException();
        }
    }
}
