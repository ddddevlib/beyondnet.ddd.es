using BeyondNet.Ddd.Es.Domain.Entities;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql
{
    public class AggregateRootRepository : IAggregateRootRepository
    {
        public Task Add(SampleAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(SampleAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SampleAggregateRoot>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SampleAggregateRoot> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(SampleAggregateRoot entity)
        {
            throw new NotImplementedException();
        }
    }
}
