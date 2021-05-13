using System;
using JetBrains.Annotations;

namespace TBureck.BuildingBlocks.Domain
{
    
    public class EntityNotFoundException : Exception
    {
        
        [PublicAPI]
        public Type EntityType { get; }
        [PublicAPI]
        public TypedGuidId Id { get; }

        public EntityNotFoundException(Type entityType, TypedGuidId id, Exception? innerException = null) 
            : base(
                $"The entity of type {entityType.FullName} with ID {id.Guid} could not be found.", 
                innerException
            )
        {
            this.EntityType = entityType;
            this.Id = id;
        }
    }
}
