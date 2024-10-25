using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record PostLikedEvent(Guid aggregateId) : DomainEvent;
}