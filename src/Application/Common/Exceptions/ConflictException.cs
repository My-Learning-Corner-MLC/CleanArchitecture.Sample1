using System.Net;

namespace Sample1.Application.Common.Exceptions;

public class ConflictException : CustomException
{
    public ConflictException() : base(
        errorMessage: "Resource items conflicted", 
        statusCode: HttpStatusCode.Conflict
    ) 
    { }

    public ConflictException(string errorMessage) : base(
        errorMessage, 
        statusCode: HttpStatusCode.Conflict
    ) 
    { }

    public ConflictException(string errorMessage, string errorDescription) : base(
        errorMessage, 
        errorDescription, 
        statusCode: HttpStatusCode.Conflict
    )
    { }
}