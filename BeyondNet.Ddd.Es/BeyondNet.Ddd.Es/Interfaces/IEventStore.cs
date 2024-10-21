namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStore
    {
        Task Save(AggregateRoot aggregate);
    }
}
