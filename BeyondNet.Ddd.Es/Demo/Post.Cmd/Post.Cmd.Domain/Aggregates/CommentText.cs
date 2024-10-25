using BeyondNet.Ddd.ValueObjects.Common;

namespace Post.Cmd.Domain.Aggregates
{
    public class CommentText : StringValueObject
    {
        protected CommentText(string value) : base(value)
        {
        }

        public static CommentText Create(string value)
        {
            return new CommentText(value);
        }
    }
}
