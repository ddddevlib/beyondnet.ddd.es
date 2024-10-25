using BeyondNet.Cqrs.Commands.Interfaces;

namespace BeyondNet.Cqrs.Commands.Impl
{
    public class AbstractCommandDispatcher : ICommandDispatcher
    {

        private readonly Dictionary<Type, Func<AbstractCommand, Task>> _handlers = new();

        public void RegisterHandler<T>(Func<T, Task> handler) where T : AbstractCommand
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (_handlers.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Handler for {typeof(T).Name} is already registered.");

            _handlers.Add(typeof(T), command => handler((T)command));
        }

        public async Task SendAsync(AbstractCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (_handlers.TryGetValue(command.GetType(), out var handler))
            {
                await handler(command);
            }
            else
            {
                throw new InvalidOperationException($"Handler for {command.GetType().Name} is not registered.");
            }
        }
    }
}
