using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands
{
    public class EditMessageCommandHandler : AbstractCommandHandler<EditMessageCommand>
    {
        private readonly IEventSourcingHandler<PostAggregate> eventSourcingHandler;

        public EditMessageCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler, ILogger<AbstractCommandHandler<EditMessageCommand>> logger) : base(logger)
        {
            this.eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
        }

        public async override Task Handle(EditMessageCommand command)
        {
            var post = await eventSourcingHandler.GetByIdAsync(command.AggregateId);

            post.EditMessage(PostMessage.Create(command.Message));

            if (!post.IsValid())
            {
                logger.LogError($"Post with id {command.Id} could not be edited. Errors: {post.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            await eventSourcingHandler.SaveAsync(post);
        }
    }
}
