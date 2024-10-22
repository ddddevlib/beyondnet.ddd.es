namespace BeyondNet.Ddd.Es.Models
{
    public class EventDataRecord
    {
        public string Id { get; set; } = default!;
        public int Version { get; set; } = default!;
        public string AggregateId { get; set; } = default!;
        public string EventName { get; set; } = default!;
        public string EventData { get; set; } = default!;
        public int Sequence { get; private set; }
        public string AssemblyQualifyName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
