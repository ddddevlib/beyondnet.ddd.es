namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStore
    {
        Task SaveAsync(Guid aggregateId, string aggregateType, ICollection<IDomainEvent> events, int expectedVersion);
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(Guid aggregateId);
    }
}
