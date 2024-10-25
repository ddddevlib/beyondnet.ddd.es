using BeyondNet.Cqrs.Commons.Interfaces;

namespace BeyondNet.Cqrs.Commons.Impl
{
    public abstract class AbstractMessage : IMessage
    {
        public Guid Id { get; set; }
    }
}
