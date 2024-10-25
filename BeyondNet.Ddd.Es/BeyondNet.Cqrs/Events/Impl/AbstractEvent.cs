using BeyondNet.Cqrs.Commons.Impl;

namespace BeyondNet.Cqrs.Events.Impl
{
    public abstract class AbstractEvent : AbstractMessage
    {
        public int Version { get; set; } = default!;
        public string Type { get; set; } = default!;

        protected AbstractEvent(string type)
        {
            Type = type;
        }
    }
}
