using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStore<TAggregate>
            where TAggregate : AggregateRoot<TAggregate, IProps>
    {
        Task<TAggregate> Load(TAggregate aggregate);
        Task Save(TAggregate aggregate);
    }
}
