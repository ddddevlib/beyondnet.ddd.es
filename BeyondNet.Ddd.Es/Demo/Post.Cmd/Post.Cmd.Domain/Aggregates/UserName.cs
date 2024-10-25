using BeyondNet.Ddd.ValueObjects.Common;

namespace Post.Cmd.Domain.Aggregates
{
    public class UserName : StringValueObject
    {
        protected UserName(string value) : base(value)
        {
        }

        public static UserName Create(string value)
        {
            return new UserName(value);
        }
    }
}
