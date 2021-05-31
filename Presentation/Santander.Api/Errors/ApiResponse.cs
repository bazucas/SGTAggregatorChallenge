namespace Santander.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        /// <summary>
        /// Gets the default message for status code.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <returns></returns>
        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "HN: You have made a bad request",
                404 => "HN: Resource was not found",
                500 => "HN: Server error",
                _ => null
            };
        }
    }
}
