using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using Post.Cmd.Api.Commands;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.CommandHandlers
{
    public class LikePostCommandHandler : AbstractCommandHandler<LikePostCommand>
    {
        private readonly IEventSourcingHandler<PostAggregate> eventSourcingHandler;

        public LikePostCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler, ILogger<AbstractCommandHandler<LikePostCommand>> logger) : base(logger)
        {
            this.eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
        }

        public async override Task Handle(LikePostCommand command)
        {
            var post = await eventSourcingHandler.GetByIdAsync(command.AggregateId);
            
            post.LikePost();

            if (!post.IsValid())
            {
                logger.LogError($"Post with id {command.Id} could not be liked. Errors: {post.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            await eventSourcingHandler.SaveAsync(post);
        }
    }
}
