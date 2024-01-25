using System.Net;

namespace Sample1.Application.Common.Exceptions
{
    public class CustomException : Exception
    {
        public string? ErrorDetailMessage { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(
            string errorMessage,
            string? errorDescription = default,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError
        ) : base(errorMessage)
        {
            ErrorDetailMessage = errorDescription;
            StatusCode = statusCode;
        }
    }
}