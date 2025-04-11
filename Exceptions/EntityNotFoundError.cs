using Microsoft.Identity.Client;

namespace wikiAPI.Exceptions
{
    /// <summary>
    /// Exception thrown when an entity is not found in the database.
    /// </summary>
    public class EntityNotFoundError : Exception
    {
        /// <summary>
        /// Constructor for just the message.
        /// </summary>
        /// <param name="message">The custom message</param>
        /// <returns>The EntityNotFoundError</returns>
        public EntityNotFoundError(string message) : base(message)
        {
        }
    }
}