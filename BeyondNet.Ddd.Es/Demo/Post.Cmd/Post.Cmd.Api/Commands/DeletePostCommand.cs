using BeyondNet.Cqrs.Commands.Impl;

namespace Post.Cmd.Api.Commands
{
    public class DeletePostCommand : AbstractCommand
    {
        Guid AggregateId { get; set; }
        public string Username { get; set; }
    }
}