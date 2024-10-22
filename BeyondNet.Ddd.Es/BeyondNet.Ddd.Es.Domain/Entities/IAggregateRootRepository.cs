namespace BeyondNet.Ddd.Es.Domain.Entities
{
    public interface IAggregateRootRepository
    {
       Task<IEnumerable<SampleAggregateRoot>> GetAll();
        Task<SampleAggregateRoot> GetById(Guid id);
        Task Add(SampleAggregateRoot entity);
        Task Update(SampleAggregateRoot entity);
        Task Delete(SampleAggregateRoot entity);
    }
}
