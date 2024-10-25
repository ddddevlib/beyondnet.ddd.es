using BeyondNet.Cqrs.Commands.Impl;

namespace Post.Cmd.Api.Commands
{
    public class EditMessageCommand : AbstractCommand
    {
        public Guid AggregateId { get; set; }
        public string Message { get; set; }
    }
}