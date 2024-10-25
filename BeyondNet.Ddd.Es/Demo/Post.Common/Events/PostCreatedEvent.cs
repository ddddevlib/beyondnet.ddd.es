using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record PostCreatedEvent(Guid AggregateId, string Author, string Message, DateTime DatePosted) : DomainEvent
    {        
    }
}