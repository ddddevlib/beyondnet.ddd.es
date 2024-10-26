namespace BeyondNet.Ddd.Es.Models
{
    public class EventDataRecord
    {
        public string Id { get; set; } = default!;
        public Guid AggregateId { get; set; } = default!;
        public string AggregateType { get; set; } = default!;
        public int Version { get; set; } = default!;
        public string EventType { get; set; } = default!;
        public IDomainEvent EventData { get; set; } = default!;
        public string AssemblyQualifyName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
