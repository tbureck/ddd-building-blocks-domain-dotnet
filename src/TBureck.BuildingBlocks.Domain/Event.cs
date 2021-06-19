namespace TBureck.BuildingBlocks.Domain
{
    
    /// <summary>
    /// A simple port for handling domain events. Use the Raised event to subscribe to an event for a specific type. Use
    /// the Raise method to publish an event from your domain.
    /// </summary>
    /// <typeparam name="T">The type of the event data</typeparam>
    public static class Event<T>
    {
        
        public delegate void SimpleEventHandler(T eventData);
        
        public static event SimpleEventHandler? Raised;

        public static void Raise(T eventData)
        {
            Raised?.Invoke(eventData);
        }
    }
}