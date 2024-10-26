using BeyondNet.Cqrs.Queries.Impl;

namespace Post.Query.Api.Queries
{
    public class FindPostsByAuthorQuery : AbstractQuery
    {
        public string Author { get; set; }
    }
}