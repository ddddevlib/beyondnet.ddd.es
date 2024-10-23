namespace BeyondNet.Ddd.Es.ConsoleApp.Entities
{
    public interface IAggregateRootRepository
    {
       Task Add(SampleAggregateRoot entity);
    }
}
