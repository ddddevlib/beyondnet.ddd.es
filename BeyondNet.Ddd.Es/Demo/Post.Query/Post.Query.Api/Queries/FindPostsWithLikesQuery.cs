using BeyondNet.Cqrs.Queries.Impl;

namespace Post.Query.Api.Queries
{
    public class FindPostsWithLikesQuery : AbstractQuery
    {
        public int NumberOfLikes { get; set; }
    }
}