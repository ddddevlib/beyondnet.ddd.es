namespace BeyondNet.Cqrs.Commands.Interfaces
{
    public interface IEventSourcingHandler<TAggregate> where TAggregate : IAggregateRoot
    {
        Task SaveAsync(TAggregate aggregateRoot);
        Task<TAggregate> GetByIdAsync(Guid aggregateId);
    }
}
