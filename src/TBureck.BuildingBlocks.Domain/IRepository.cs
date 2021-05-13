using System.Threading.Tasks;
using JetBrains.Annotations;

namespace TBureck.BuildingBlocks.Domain
{
    
    /// <summary>
    /// Repositories play an important role in storing and receiving entities in/from a data source. Whenever you fetch
    /// an entity from your repository it should not be needed to add it again, the state of it should be tracked by the
    /// implementation of the repository (or accompanying classes).
    ///
    /// When using this interface you will usually want to create an intermediate interface that is specifically suited
    /// for your entity. This way you can extend the repository with methods tailored for your entity.
    ///
    /// As repositories are often implemented using IO operations in the background, they should use the async/await
    /// pattern. As such the methods in this interface are declared appropriately as well.
    ///
    /// NextId is not an asynchronous operation as it is expected that the application itself will determine the ID to
    /// use instead of any external systems like databases. Unless you have a CPU-heavy way of creating your IDs
    /// asynchronicity should not be needed here.
    /// </summary>
    /// <typeparam name="TEntityClass">The type of the entity this repository should handle</typeparam>
    /// <typeparam name="TIdClass">The type of the ID that the entity is identified by</typeparam>
    [PublicAPI]
    public interface IRepository<TEntityClass, TIdClass>
    {

        /// <summary>
        /// Create and return the next ID to use. Using ID classes based on TypedGuidId you will usually want to create
        /// a new GUID here and put it into your specific ID class object.
        /// </summary>
        /// <returns>The next ID to be used for an entity of this repository</returns>
        TIdClass NextId();

        /// <summary>
        /// Find a specific entity with the given ID. It should be assumed that the entity is known since the consumer
        /// of this method knows its ID already.
        /// </summary>
        /// <param name="id">The known ID of the entity</param>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <returns>The entity with the given ID</returns>
        Task<TEntityClass> FindByIdAsync(TIdClass id);

        /// <summary>
        /// Adds an entity to this repository. This should persist the entity according to the repository's
        /// implementation.
        /// </summary>
        /// <param name="entity">The entity to be added to the repository</param>
        Task AddAsync(TEntityClass entity);
    }
}
