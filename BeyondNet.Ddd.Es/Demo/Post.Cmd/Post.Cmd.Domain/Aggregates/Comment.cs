using BeyondNet.Ddd;
using BeyondNet.Ddd.Interfaces;

namespace Post.Cmd.Domain.Aggregates
{
    public class CommentProps : IProps
    {
        public CommentText Comment { get; private set; }
        public UserName UserName { get; private set; }

        public CommentProps(CommentText comment, UserName userName)
        {
            Comment = comment;
            UserName = UserName;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class Comment : Entity<Comment, CommentProps>
    {
        protected Comment(CommentProps props) : base(props)
        {
        }

        public static Comment Create(CommentText comment, UserName userName)
        {
            return new Comment(new CommentProps(comment, userName));
        }
    }
}
