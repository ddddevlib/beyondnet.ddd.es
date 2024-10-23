namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<ICollection<EventDataRecord>> Load(string aggregateId);
        Task Save(EventDataRecord eventData);
    }
}
