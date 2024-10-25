using BeyondNet.Cqrs.Commands.Impl;

namespace Post.Cmd.Api.Commands
{
    public class LikePostCommand : AbstractCommand
    {
        public Guid AggregateId { get; set; }
    }
}