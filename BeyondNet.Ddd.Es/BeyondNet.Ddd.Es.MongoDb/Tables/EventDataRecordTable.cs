using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeyondNet.Ddd.Es.MongoDb.Tables
{
    public class EventDataRecordTable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;        
        public Guid AggregateId { get; set; } = default!;
        public string AggregateType { get; set; } = default!;
        public int Version { get; set; } = default!;
        public string EventType { get; set; } = default!;
        public string EventData { get; set; } = default!;
        public string AssemblyQualifyName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        internal object ToEventDataRecord()
        {
            throw new NotImplementedException();
        }
    }
}
