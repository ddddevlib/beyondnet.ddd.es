using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BeyondNet.Ddd.Es.EntityFrameworkSql.Tables;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql.Configurations
{
    public class AggregateRootTableTypeConfiguration : IEntityTypeConfiguration<AggregateRootTable>
    {
        public void Configure(EntityTypeBuilder<AggregateRootTable> builder)
        {
            builder.ToTable("aggregateroots");
            builder.HasKey(x => x.Id);
        }
    }
}
