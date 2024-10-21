using BeyondNet.Ddd.Interfaces;

namespace BeyondNet.Ddd.Es.Interfaces
{
    public class AggregateRoot
    {
        public string Id { get; set; }

        /// <summary>
        /// Gets the domain events associated with the entity.
        /// </summary>
        /// <returns>The domain events associated with the entity.</returns>
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a domain event to the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to add.</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a domain event from the entity.
        /// </summary>
        /// <param name="eventItem">The domain event to remove.</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            throw new NotImplementedException();
        }

        public void LoadDomainEvents(IReadOnlyCollection<IDomainEvent> history)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears all domain events associated with the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            throw new NotImplementedException();
        }
    }
}