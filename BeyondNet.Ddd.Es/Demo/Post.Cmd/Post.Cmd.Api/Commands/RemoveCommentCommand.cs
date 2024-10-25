using BeyondNet.Cqrs.Commands.Impl;

namespace Post.Cmd.Api.Commands
{
    public class RemoveCommentCommand : AbstractCommand
    {
        public Guid AggregateId { get; set; }
        public Guid CommentId { get; set; }
        public string Username { get; set; }
    }
}