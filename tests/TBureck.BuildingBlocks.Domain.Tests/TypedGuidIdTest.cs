using System;
using Xunit;

namespace TBureck.BuildingBlocks.Domain
{
    
    public class TypedGuidIdTest
    {
        
        private class EntityId : TypedGuidId
        {
            
            public EntityId(Guid guid) : base(guid) { }
        }

        private class OtherId : TypedGuidId
        {
            
            public OtherId(Guid guid) : base(guid) { }
        }

        [Fact]
        public void CreateNew_SavesGuid()
        {
            Guid guid = Guid.NewGuid();
            EntityId entityId = new(guid);
            
            Assert.Equal(guid, entityId.Guid);
        }

        [Fact]
        public void CreateNew_EmptyGuid_ThrowsArgumentException()
        {
            ArgumentException argumentException = Assert.Throws<ArgumentException>(() => new EntityId(Guid.Empty));
            
            Assert.Equal("Id value must not be empty.", argumentException.Message);
        }

        [Fact]
        public void IEquatableEquals_SameGuid_ReturnsTrue()
        {
            Guid guid = Guid.NewGuid();

            EntityId entityId1 = new(guid);
            EntityId entityId2 = new(guid);
            
            Assert.True(entityId1.Equals(entityId2));
        }

        [Fact]
        public void IEquatableEquals_DifferentGuids_ReturnsFalse()
        {
            EntityId entityId1 = new(Guid.NewGuid());
            EntityId entityId2 = new(Guid.NewGuid());
            
            Assert.False(entityId1.Equals(entityId2));
        }

        [Fact]
        public void IEquatableEquals_DifferentTypedIds_ReturnsFalse()
        {
            Guid guid = Guid.NewGuid();
            
            EntityId entityId = new(guid);
            OtherId otherId = new(guid);
            
            Assert.False(entityId.Equals(otherId));
        }

        [Fact]
        public void ObjectEquals_SameGuid_ReturnsTrue()
        {
            Guid guid = Guid.NewGuid();

            object entityId1 = new EntityId(guid);
            object entityId2 = new EntityId(guid);
            
            Assert.True(entityId1.Equals(entityId2));
        }

        [Fact]
        public void ObjectEquals_DifferentGuids_ReturnsFalse()
        {
            object entityId1 = new EntityId(Guid.NewGuid());
            object entityId2 = new EntityId(Guid.NewGuid());
            
            Assert.False(entityId1.Equals(entityId2));
        }

        [Fact]
        public void ObjectEquals_DifferentObject_ReturnsFalse()
        {
            object entityId = new EntityId(Guid.NewGuid());
            const string somethingElse = "is not an id";
            
            Assert.False(entityId.Equals(somethingElse));
        }

        [Fact]
        public void ObjectEquals_OtherIsNull_ReturnsFalse()
        {
            object entityId = new EntityId(Guid.NewGuid());

            Assert.False(entityId.Equals(null));
        }

        [Fact]
        public void ObjectEquals_DifferentTypedIds_ReturnsFalse()
        {
            Guid guid = Guid.NewGuid();
            
            object entityId = new EntityId(guid);
            object otherId = new OtherId(guid);
            
            Assert.False(entityId.Equals(otherId));
        }

        [Fact]
        public void OperatorEquals_SameGuid_ReturnsTrue()
        {
            Guid guid = Guid.NewGuid();

            EntityId entityId1 = new(guid);
            EntityId entityId2 = new(guid);
            
            Assert.True(entityId1 == entityId2);
        }

        [Fact]
        public void OperatorEquals_DifferentTypedIds_ReturnsFalse()
        {
            Guid guid = Guid.NewGuid();
            
            EntityId entityId = new(guid);
            OtherId otherId = new(guid);
            
            Assert.False(entityId == otherId);
        }

        [Fact]
        public void OperatorEquals_LeftIdIsNull_ReturnsFalse()
        {
            EntityId entityId = new(Guid.NewGuid());
            
            Assert.False(null == entityId);
        }

        [Fact]
        public void OperatorEquals_RightIdIsNull_ReturnsFalse()
        {
            EntityId entityId = new(Guid.NewGuid());
            
            Assert.False(entityId == null);
        }

        [Fact]
        public void OperatorEquals_BothIdsAreNull_ReturnsTrue()
        {
            EntityId entityId = null;

            Assert.True(entityId == null);
        }

        [Fact]
        public void GetHashCode_ReturnsGuidsHashCode()
        {
            Guid guid = Guid.NewGuid();
            EntityId entityId = new(guid);
            
            Assert.Equal(guid.GetHashCode(), entityId.GetHashCode());
        }
    }
}
