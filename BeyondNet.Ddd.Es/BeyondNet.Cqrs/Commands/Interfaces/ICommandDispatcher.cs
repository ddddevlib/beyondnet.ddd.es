using BeyondNet.Cqrs.Commands.Impl;

namespace BeyondNet.Cqrs.Commands.Interfaces
{
    public interface ICommandDispatcher
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : AbstractCommand;

        Task SendAsync(AbstractCommand command);
    }
}
