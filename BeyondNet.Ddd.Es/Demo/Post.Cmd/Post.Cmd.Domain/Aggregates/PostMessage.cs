using BeyondNet.Ddd.ValueObjects.Common;

namespace Post.Cmd.Domain.Aggregates
{
    public class PostMessage : StringValueObject
    {
        public PostMessage(string value) : base(value)
        {
        }

        public static PostMessage Create(string value)
        {
            return new PostMessage(value);
        }
    }
}
