using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands
{
    public class DeletePostCommandHandler : AbstractCommandHandler<DeletePostCommand>
    {
        private readonly IEventSourcingHandler<PostAggregate> eventSourcingHandler;

        public DeletePostCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler, ILogger<AbstractCommandHandler<DeletePostCommand>> logger) : base(logger)
        {
            this.eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
        }

        public async override Task Handle(DeletePostCommand command)
        {
            var post = await eventSourcingHandler.GetByIdAsync(command.Id);

            if (post == null)
            {
                throw new Exception($"Post with id {command.Id} not found");
            }

            post.DeletePost(command.Username);

            if(!post.IsValid())
            {
                logger.LogError($"Post with id {command.Id} could not be deleted. Errors: {post.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            await eventSourcingHandler.SaveAsync(post);
        }
    }
}
