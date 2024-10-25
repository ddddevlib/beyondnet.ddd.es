using BeyondNet.Cqrs.Commands.Impl;

namespace Post.Cmd.Api.Commands
{
    public class NewPostCommand : AbstractCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}