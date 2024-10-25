using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands
{
    public class NewPostCommandHandler : AbstractCommandHandler<NewPostCommand>
    {
        private readonly IEventSourcingHandler<PostAggregate> eventSourcingHandler;

        public NewPostCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler, ILogger<AbstractCommandHandler<NewPostCommand>> logger) : base(logger)
        {
            this.eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
        }

        public override async Task Handle(NewPostCommand command)
        {
            var post = PostAggregate.Create(AuthorName.Create(command.Author), PostMessage.Create(command.Message));

            if (!post.IsValid())
            {
                logger.LogError($"Post is not valid.Errors:{post.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            await eventSourcingHandler.SaveAsync(post);
        }
    }
}
