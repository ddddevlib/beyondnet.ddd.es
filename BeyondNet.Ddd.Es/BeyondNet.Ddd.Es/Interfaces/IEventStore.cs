using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStore<TAggregate,TProps>
                where TAggregate : class
                where TProps : class, IProps
    {
        Task<TAggregate> Load(TAggregate aggregate);
        Task Save(TAggregate aggregate);
    }
}
