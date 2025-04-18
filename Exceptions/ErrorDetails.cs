namespace wikiAPI.Exceptions
{
    /// <summary>
    /// Represents the details of an error response.
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ExceptionMessage { get; set; }
    }
}