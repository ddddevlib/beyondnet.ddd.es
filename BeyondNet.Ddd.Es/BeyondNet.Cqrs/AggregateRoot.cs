using System.Reflection;
using BeyondNet.Cqrs.Events.Impl;

namespace BeyondNet.Cqrs
{
    public abstract class AggregateRoot
    {
        protected Guid _id { get; set; }

        private readonly List<AbstractEvent> _changes = new();

        public Guid Id => _id;

        public int Version { get; set; } = -1;

        public IEnumerable<AbstractEvent> GetUncommittedChanges() => _changes;

        public void MarkChangesAsCommitted() => _changes.Clear();

        public void LoadFromHistory(IEnumerable<AbstractEvent> history)
        {
            foreach (var e in history)
            {
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(AbstractEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(AbstractEvent @event, bool isNew)
        {
            var method = GetType().GetMethod("Apply", new Type[] { @event.GetType() });

            if (method == null)
            {
                throw new InvalidOperationException($"Missing Apply method for {@event.GetType()}");
            }

            method.Invoke(this, new object[] { @event });

            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        protected void RaiseEvent(AbstractEvent @event)
        {
            ApplyChange(@event, true);
        }

        public void ReplayEvents(IEnumerable<AbstractEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event);
            }
        }
    }
}
