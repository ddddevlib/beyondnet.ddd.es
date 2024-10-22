using BeyondNet.Ddd.Es.EntityFrameworkSql.Configurations;
using BeyondNet.Ddd.Es.EntityFrameworkSql.Tables;
using BeyondNet.Ddd.Es.Models;
using Microsoft.EntityFrameworkCore;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql
{
    public class EsDbContext : DbContext
    {
        public DbSet<EventDataRecord> EventDataRecords { get; set; }
        public DbSet<ErrorDataRecord> ErrorDataRecords { get; set; }
        public DbSet<AggregateRootTable> AggregateRootTables { get; set; }


        public EsDbContext(DbContextOptions<EsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DddEventDataStore");

            modelBuilder.ApplyConfiguration(new ErrorDataRecordTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventDataRecordTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AggregateRootTableTypeConfiguration());
        }
    }
}
