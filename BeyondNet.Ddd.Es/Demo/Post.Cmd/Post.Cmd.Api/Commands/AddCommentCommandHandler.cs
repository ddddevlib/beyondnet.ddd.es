using BeyondNet.Cqrs.Commands.Impl;
using BeyondNet.Cqrs.Commands.Interfaces;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Api.Commands
{
    public class AddCommentCommandHandler : AbstractCommandHandler<AddCommentCommand>
    {
        private readonly IEventSourcingHandler<PostAggregate> eventSourcingHandler;

        public AddCommentCommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler, ILogger<AbstractCommandHandler<AddCommentCommand>> logger) : base(logger)
        {
            this.eventSourcingHandler = eventSourcingHandler ?? throw new ArgumentNullException(nameof(eventSourcingHandler));
        }

        public async override Task Handle(AddCommentCommand command)
        {
            var post = await eventSourcingHandler.GetByIdAsync(command.AggregateId);

            var comment = Comment.Create(CommentText.Create(command.Comment), UserName.Create(command.Username));

            if (!comment.IsValid())
            {
                logger.LogError($"Comment is not valid. Errors: {comment.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            post.AddComment(comment);

            if (!post.IsValid())
            {
                logger.LogError($"Post is not valid. Errors: {post.BrokenRules.GetBrokenRulesAsString()}");
                return;
            }

            await eventSourcingHandler.SaveAsync(post);
        }
    }
}
