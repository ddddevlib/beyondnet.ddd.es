namespace BeyondNet.Ddd.Es.EntityFrameworkSql.Tables
{
    public class AggregateRootTable
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string EntityRef1 { get; set; } = default!;
        public string EntityRef2 { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string CreatedAt { get; set; } = default!;
        public string CreatedBy { get; set; } = default!;
        public string UpdatedAt { get; set; } = default!;
        public string UpdatedBy { get; set; } = default!;
        public string TimeSpan { get; set; } = default!;
    }
}
