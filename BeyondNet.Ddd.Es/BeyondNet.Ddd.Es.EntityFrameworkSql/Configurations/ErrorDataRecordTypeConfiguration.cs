using BeyondNet.Ddd.Es.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql.Configurations
{
    public class ErrorDataRecordTypeConfiguration : IEntityTypeConfiguration<ErrorDataRecord>
    {
        public void Configure(EntityTypeBuilder<ErrorDataRecord> builder)
        {
            builder.ToTable("eventdataerrors");
            builder.HasKey(x => x.Id);
        }
    }
}
