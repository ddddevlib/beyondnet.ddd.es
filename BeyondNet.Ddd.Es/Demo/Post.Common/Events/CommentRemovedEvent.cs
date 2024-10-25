using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record CommentRemovedEvent(Guid AggregateId, Guid CommentId): DomainEvent;
}