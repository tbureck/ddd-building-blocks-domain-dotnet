using System;
using Xunit;

namespace TBureck.BuildingBlocks.Domain
{
    
    public class EntityNotFoundExceptionTest
    {

        private class EntityId : TypedGuidId
        {
            
            public EntityId(Guid guid) : base(guid) { }
        }

        private class Entity { }

        [Fact]
        public void MinimalConstructor_SetsCorrectMessage()
        {
            Guid guid = Guid.NewGuid();
            EntityNotFoundException exception = new(typeof(Entity), new EntityId(guid));
            
            Assert.Equal(
                $"The entity of type {typeof(Entity)} with ID {guid} could not be found.",
                exception.Message
            );
        }

        [Fact]
        public void CauseConstructor_SetsInnerException()
        {
            Exception innerException = new();
            EntityNotFoundException exception = new(typeof(Entity), new EntityId(Guid.NewGuid()), innerException);
            
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
