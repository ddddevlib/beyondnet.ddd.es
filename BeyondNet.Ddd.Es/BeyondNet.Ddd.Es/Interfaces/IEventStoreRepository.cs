namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStoreRepository
    {
        Task<ICollection<EventDataRecord>> Load(Guid aggregateId);
        Task SaveAsync(EventDataRecord @event);
    }
}
