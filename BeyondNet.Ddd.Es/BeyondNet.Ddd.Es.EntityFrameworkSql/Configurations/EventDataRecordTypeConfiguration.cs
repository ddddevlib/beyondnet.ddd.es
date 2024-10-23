using BeyondNet.Ddd.Es.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql.Configurations
{
    public class EventDataRecordTypeConfiguration : IEntityTypeConfiguration<EventDataRecord>
    {
        public void Configure(EntityTypeBuilder<EventDataRecord> builder)
        {
            builder.ToTable("eventdatarecords");
            builder.HasKey(x => x.Id);
        }
    }
}
