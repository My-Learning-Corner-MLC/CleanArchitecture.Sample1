using System.Net;
using Sample1.Application.Common.Constants;

namespace Sample1.Application.Common.Exceptions;

public class NotFoundException : CustomException
{
    public NotFoundException() : base(
        errorMessage: ExceptionConst.ErrorMessages.RESOURCE_NOT_FOUND, 
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