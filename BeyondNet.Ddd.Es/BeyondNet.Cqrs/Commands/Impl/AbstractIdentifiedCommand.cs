namespace BeyondNet.Cqrs.Commands.Impl
{
    public class AbstractIdentifiedCommand<T, R> : IRequest<R>
       where T : IRequest<R>
    {
        public T Command { get; }
        public Guid Id { get; }
        public AbstractIdentifiedCommand(T command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
