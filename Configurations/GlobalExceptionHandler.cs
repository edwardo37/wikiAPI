using System.ComponentModel.DataAnnotations;
using System.Net;
using wikiAPI.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace wikiAPI.Configurations
{
    /// <summary>
    /// The interceptor before the error reaches the requester
    /// </summary>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Catches the exception and handles it by returning an ErrorDetails object.
        /// </summary>
        /// <param name="httpContext">Information about the HTTP request</param>
        /// <param name="exception">Exception that has occured</param>
        /// <param name="cancellationToken">Stops the program from endlessly requesting nothing</param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ErrorDetails errorDetails = new ErrorDetails();

            // The entity could not be found
            if (exception is EntityNotFoundError)
            {
                errorDetails.StatusCode = (int)HttpStatusCode.NotFound;
                errorDetails.Message = "Entity was not found.";
                errorDetails.ExceptionMessage = exception.Message;
            }
            // The input was invalid
            else if (exception is InvalidInputError)
            {
                errorDetails.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails.Message = GenerateInvalidInputMessage((InvalidInputError)exception);
                errorDetails.ExceptionMessage = exception.Message;
            }
            // Something else
            else
            {
                errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorDetails.Message = "An unexpected error occurred.";
                errorDetails.ExceptionMessage = exception.Message;
            }

            // Set the HTTP status that will be seen by the requester
            httpContext.Response.StatusCode = errorDetails.StatusCode;
            httpContext.Response.ContentType = "application/json";

            // Write the error details as JSON
            await httpContext.Response.WriteAsJsonAsync(errorDetails, cancellationToken);

            // Task completed successfully
            return true;
        }

        /// <summary>
        /// Generates a descriptive error message with all errors in input
        /// </summary>
        /// <param name="exception">The exception to pass in</param>
        /// <returns>A formatted string with the error message and all errors associated</returns>
        private string GenerateInvalidInputMessage(InvalidInputError exception)
        {
            string message = exception.Message;

            List<string> errors = exception.ModelState.Values.SelectMany(
                v => v.Errors.Select(e => e.ErrorMessage)
                ).ToList();

            message = string.Join(", ", errors);

            return message;
        }
    }
}