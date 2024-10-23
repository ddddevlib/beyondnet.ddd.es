namespace BeyondNet.Ddd.Es.Interfaces
{
    public interface IEventStore<TAggregateRoot>
                where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> Load(TAggregateRoot aggregate);
        Task Save(TAggregateRoot aggregate);
    }
}
