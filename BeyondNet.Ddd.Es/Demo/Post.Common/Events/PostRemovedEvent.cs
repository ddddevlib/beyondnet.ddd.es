using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record PostRemovedEvent(Guid aggregateId) : DomainEvent;
}