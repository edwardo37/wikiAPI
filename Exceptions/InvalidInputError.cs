using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace wikiAPI.Exceptions
{
    /// <summary>
    /// When the requester provides invalid input
    /// </summary>
    public class InvalidInputError : Exception
    {
        public ModelStateDictionary ModelState { get; set; }

        /// <summary>
        /// Constructor for the message and model state
        /// </summary>
        /// <param name="message">Custom error message</param>
        /// <param name="modelState">ModelStateDictionary containing validation errors</param>
        /// <returns>The InvalidInputError<returns>
        public InvalidInputError(string message, ModelStateDictionary modelState) : base(message)
        {
            ModelState = modelState;
        }
    }
}