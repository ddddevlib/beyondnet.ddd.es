using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands
{
    public class RemoveCommentCommandHandler : AbstractCommandHandler<RemoveCommentCommand>
    {
        private readonly IEventSourcingHandler<PostAggregate> eventSourcingHandler;

        public RemoveCommentCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler, ILogger<AbstractCommandHandler<RemoveCommentCommand>> logger) : base(logger)
        {
            this.eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
        }

        public async override Task Handle(RemoveCommentCommand command)
        {
            var post = await eventSourcingHandler.GetByIdAsync(command.AggregateId);

            if (post != null) {
                logger.LogError($"Post not found. Id: {command.AggregateId}");
                return;
            }

            post.RemoveComment(command.CommentId);

            if (!post.IsValid())
            {
                logger.LogError($"Post is not valid. Errors: {post.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            await eventSourcingHandler.SaveAsync(post);
        }
    }
}
