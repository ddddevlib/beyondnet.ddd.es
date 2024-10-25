using BeyondNet.Ddd.Es.EntityFrameworkSql.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql.Configurations
{
    public class EventDataRecordTypeConfiguration : IEntityTypeConfiguration<EventDataRecordTable>
    {
        public void Configure(EntityTypeBuilder<EventDataRecordTable> builder)
        {
            builder.ToTable("eventdatarecords");
            builder.HasKey(x => x.Id);
        }
    }
}
