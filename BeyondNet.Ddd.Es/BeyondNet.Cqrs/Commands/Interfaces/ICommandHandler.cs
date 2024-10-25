using BeyondNet.Cqrs.Commands.Impl;

namespace BeyondNet.Cqrs.Commands.Interfaces
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(AbstractCommand command);
    }
}
