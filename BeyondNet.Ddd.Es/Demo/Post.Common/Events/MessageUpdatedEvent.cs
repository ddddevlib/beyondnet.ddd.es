using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record MessageUpdatedEvent(Guid aggregateId, string Message) : DomainEvent
    {        
    }
}