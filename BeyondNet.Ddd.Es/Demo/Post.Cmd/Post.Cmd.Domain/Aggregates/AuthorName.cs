using BeyondNet.Ddd;

namespace Post.Cmd.Domain.Aggregates
{
    public class AuthorName : ValueObject<string>
    {

        private AuthorName(string value) : base(value)
        {
        }

        public static AuthorName Create(string value)
        {
            return new AuthorName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
