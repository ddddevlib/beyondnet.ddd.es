using BeyondNet.Ddd.Es.EntityFrameworkSql.Tables;

namespace BeyondNet.Ddd.Es.ConsoleApp
{
    public class AggregateRootRepository : IAggregateRootRepository
    {
        private readonly EsDbContext dbContext;

        public AggregateRootRepository(EsDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task Add(SampleAggregateRoot entity)
        {
            var table = new AggregateRootTable
            {
                Id = entity.Id.GetValue(),
                Name = entity.Props.Name.GetValue(),
                EntityRef1 = Guid.NewGuid().ToString(),
                EntityRef2 = Guid.NewGuid().ToString(),
                Status = entity.Props.Status.ToString(),
                CreatedBy = entity.Props.Audit.GetValue().CreatedBy,
                CreatedAt = entity.Props.Audit.GetValue().CreatedAt.ToString(),
                UpdatedBy = entity.Props.Audit.GetValue().UpdatedBy!,
                UpdatedAt = entity.Props.Audit.GetValue().UpdatedAt.ToString()!,
                TimeSpan = entity.Props.Audit.GetValue().TimeSpan.ToString()
            };

            dbContext.AggregateRootTables.Add(table);

            return dbContext.SaveChangesAsync();
        }
    }
}
