using BeyondNet.Ddd.ValueObjects.Common;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostLiked : BoolValueObject
    {
        protected PostLiked(bool value) : base(value)
        {
        }

        public static PostLiked Create(bool value)
        {
            return new PostLiked(value);
        }
    }
}
