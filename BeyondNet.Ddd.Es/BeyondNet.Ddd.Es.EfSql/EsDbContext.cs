using BeyondNet.Ddd.Es.EntityFrameworkSql.Configurations;
using BeyondNet.Ddd.Es.EntityFrameworkSql.Tables;
using BeyondNet.Ddd.Es.Models;
using Microsoft.EntityFrameworkCore;

namespace BeyondNet.Ddd.Es.EntityFrameworkSql
{
    public class EsDbContext : DbContext
    {
        public DbSet<EventDataRecordTable> EventDataRecords { get; set; }
        public EsDbContext(DbContextOptions<EsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DddEs");

            modelBuilder.ApplyConfiguration(new EventDataRecordTypeConfiguration());
        }
    }
}
