using System.Net;

namespace Sample1.Application.Common.Exceptions;

public class NotFoundException : CustomException
{
    public NotFoundException() : base(
        errorMessage: "Resource not found", 
        statusCode: HttpStatusCode.NotFound
    )
    { }

    public NotFoundException(string errorMessage) : base(
        errorMessage, 
        statusCode: HttpStatusCode.NotFound
    )
    { }

    public NotFoundException(string errorMessage, string errorDescription) : base(
        errorMessage,
        errorDescription,
        statusCode: HttpStatusCode.NotFound
    )
    { }
}