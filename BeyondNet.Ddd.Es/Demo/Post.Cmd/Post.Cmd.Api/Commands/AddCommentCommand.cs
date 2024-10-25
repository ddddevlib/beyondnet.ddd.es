using BeyondNet.Cqrs.Commands.Impl;

namespace Post.Cmd.Api.Commands
{
    public class AddCommentCommand : AbstractCommand
    {
        public Guid AggregateId { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
    }
}