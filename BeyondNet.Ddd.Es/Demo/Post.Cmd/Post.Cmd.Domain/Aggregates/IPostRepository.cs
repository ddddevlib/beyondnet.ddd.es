using BeyondNet.Ddd.Interfaces;

namespace Post.Cmd.Domain.Aggregates
{
    public interface IPostRepository : IRepository<PostAggregate>
    {
       Task Insert(PostAggregate aggregate);
    }
}
