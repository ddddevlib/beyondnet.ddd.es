using BeyondNet.Ddd;

namespace Post.Common.Events
{
    public record CommentAddedEvent(Guid aggregateId, Guid CommentId, string Comment, string Username, DateTime CommentDate): DomainEvent;
}