using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record CommentUpdatedEvent(Guid AggregateId, Guid CommentId, string Comment, string UserName, DateTime EditDate): DomainEvent;
}