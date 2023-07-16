using System;

namespace TBureck.BuildingBlocks.Domain
{
    
    /// <summary>
    /// Abstract wrapper class for a GUID. Extend this class to quickly create a typed ID value object class that uses
    /// a GUID. The GUID can be set via the protected constructor.
    /// </summary>
    public abstract class TypedGuidId : IEquatable<TypedGuidId>
    {
        
        public Guid Guid { get; }
        
        protected TypedGuidId(Guid guid)
        {
            if (guid == Guid.Empty) {
                throw new ArgumentException("Id value must not be empty.");
            }
            
            this.Guid = guid;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}:{Guid.ToString()}";
        }

        public override bool Equals(object? obj)
        {
            return obj is TypedGuidId other && Equals(other);
        }

        public bool Equals(TypedGuidId? other)
        {
            bool typesEqual = this.GetType() == other?.GetType();
            bool guidEquals = this.Guid == other?.Guid;

            return typesEqual && guidEquals;
        }

        public override int GetHashCode()
        {
            return Guid.GetHashCode();
        }

        public static bool operator ==(TypedGuidId? id1, TypedGuidId? id2)
        {
            return id1?.Equals(id2) ?? Equals(id2, null);
        }

        public static bool operator !=(TypedGuidId? id1, TypedGuidId? id2)
        {
            return !(id1 == id2);
        }
    }
}
