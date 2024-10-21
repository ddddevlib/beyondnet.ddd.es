using BeyondNet.Ddd.Es.Models;

namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStoreRepository
    {
        Task Save(EventDataRecord eventData);
    }
}
