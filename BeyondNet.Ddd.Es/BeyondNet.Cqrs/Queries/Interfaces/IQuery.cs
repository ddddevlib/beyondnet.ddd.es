namespace BeyondNet.Cqrs.Queries.Interfaces
{
    public interface IQuery<out T> : IRequest<T>
       where T : notnull
    {
    }
}
