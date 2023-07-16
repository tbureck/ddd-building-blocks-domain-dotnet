using System.Collections.Generic;

namespace TBureck.BuildingBlocks.Domain
{
    
    public interface IEventDrivenAggregate
    {

        /// <summary>
        /// List of events that have happened on this aggregate. Can be read by persistence to write to event store.
        /// </summary>
        public List<IDomainEvent> Changes { get; }
    }

    public static class EventDrivenAggregateMutators
    {
        
        /// <summary>
        /// Mutates the aggregate's state and pushes changes to both the changes list of the aggregate as well as the domain event port.
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="event">the event that has happened</param>
        /// <typeparam name="T">type of the event that has happened</typeparam>
        public static void Apply<T>(this IEventDrivenAggregate aggregate, T @event) where T : IDomainEvent
        {
            aggregate.Changes.Add(@event);
            aggregate.Mutate(@event);
            Event<T>.Raise(@event);
        }

        /// <summary>
        /// Mutates the aggregate's local state. Implement a When method that accepts the event's type to specify the actual state change.
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="event">the event that has happened</param>
        public static void Mutate(this IEventDrivenAggregate aggregate, IDomainEvent @event)
        {
            ((dynamic)aggregate).When((dynamic)@event);
        }
    }
}
