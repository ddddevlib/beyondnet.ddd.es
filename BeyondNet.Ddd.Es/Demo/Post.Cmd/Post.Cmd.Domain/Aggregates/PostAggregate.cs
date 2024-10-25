using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;
using BeyondNet.Ddd.Rules;
using BeyondNet.Ddd.ValueObjects.Common;
using Post.Common.Events;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostAggregateProps : IProps
    {
        public AuthorName Author { get; private set; }
        public PostMessage Message { get; private set; }
        public PostLiked Liked { get; set; }
        public ICollection<Comment> Comments { get; private set; }
        public PostAggregateStatus Status { get; set; }

        public PostAggregateProps(AuthorName authorName)
        {
            Author = authorName;
            Liked = PostLiked.Create(false);
            Comments = new List<Comment>();
            Status = PostAggregateStatus.Active;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class PostAggregate : AggregateRoot<PostAggregate, PostAggregateProps>
    {
        
        protected PostAggregate(PostAggregateProps props) : base(props)
        {
            if(TrackingState.IsNew)
            {
                DomainEvents.RaiseEvent(new PostCreatedEvent(Id.GetValue(), Props.Author.GetValue(), Props.Message.GetValue(), DateTime.Now));
            }
        }

        public static PostAggregate Create(AuthorName authorName, PostMessage message)
        {
            return PostAggregate.Create(authorName, message);
        }

        public void EditMessage(PostMessage message)
        {
            if (Props.Status == PostAggregateStatus.Inactive)
            {
                BrokenRules.Add(new BrokenRule("Edit","You cannot edit the message of an inactive post!"));
                return;
            }

            Props.Message.SetValue(message.GetValue());
            
            DomainEvents.RaiseEvent(new MessageUpdatedEvent(Id.GetValue(), message.GetValue()));
        }

        public void LikePost()
        {
            if (Props.Status != PostAggregateStatus.Active)
            {
                BrokenRules.Add(new BrokenRule("Like","You cannot like an inactive post!"));
                return;
            }

            Props.Liked = PostLiked.Create(true);
            DomainEvents.RaiseEvent(new PostLikedEvent(Id.GetValue()));
        }

        public void AddComment(Comment comment)
        {
            if (Props.Status != PostAggregateStatus.Active)
            {
                throw new InvalidOperationException("You cannot add a comment to an inactive post!");
            
            }
            
            Props.Comments.Add(comment);
                   
            DomainEvents.RaiseEvent(new CommentAddedEvent(Id.GetValue(), comment.Id.GetValue(), comment.Props.Comment.GetValue(), comment.Props.UserName.GetValue(), DateTime.Now));
        }

        public void RemoveComment(Guid commentId)
        {
            if (Props.Status != PostAggregateStatus.Active)
            {
                BrokenRules.Add(new BrokenRule("Remove", "You cannot remove a comment from an inactive post!"));
                return;
            }

            var comment = Props.Comments.FirstOrDefault(x => x.Id.GetValue() == commentId);

            if (comment == null)
            {
                BrokenRules.Add(new BrokenRule("Remove", "Comment not found!"));
                return;
            }

            Props.Comments.Remove(comment);

            DomainEvents.RaiseEvent(new CommentRemovedEvent(Id.GetValue(), comment.Id.GetValue()));
        }

        public void DeletePost(string username)
        {
            if (Props.Status != PostAggregateStatus.Active)
            {
                BrokenRules.Add(new BrokenRule("Delete", "You cannot delete an inactive post!"));
                return;
            }

            Props.Status = PostAggregateStatus.Delete;

            DomainEvents.RaiseEvent(new PostRemovedEvent(Id.GetValue()));
        }
    }

    public class PostAggregateStatus : Enumeration
    {
        public static PostAggregateStatus Active = new PostAggregateStatus(1, nameof(Active));
        public static PostAggregateStatus Inactive = new PostAggregateStatus(2, nameof(Inactive));
        public static PostAggregateStatus Delete = new PostAggregateStatus(0, nameof(Delete));
        public PostAggregateStatus(int id, string name) : base(id, name)
        {
        }
    }
}