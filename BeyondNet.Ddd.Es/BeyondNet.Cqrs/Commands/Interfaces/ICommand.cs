namespace BeyondNet.Cqrs.Commands.Interfaces
{
    public interface ICommand
    {
        public Guid Id { get; set; }
    }
}
