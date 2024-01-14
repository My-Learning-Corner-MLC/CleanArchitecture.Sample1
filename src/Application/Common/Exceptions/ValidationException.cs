using System.Net;
using FluentValidation.Results;

namespace Sample1.Application.Common.Exceptions;

public class ValidationException : CustomException
{
    public ValidationException() : base(
        errorMessage: "One or more validation failures have occurred.",
        statusCode: HttpStatusCode.BadRequest
    )
    { }

    public ValidationException(string errorDescription) : base(
        errorMessage: "One or more validation failures have occurred.",
        errorDescription,
        statusCode: HttpStatusCode.BadRequest
    )
    { }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base(
        errorMessage: "One or more validation failures have occurred.",
        errorDescription: failures.Select(f => f.ErrorMessage).FirstOrDefault(),
        statusCode: HttpStatusCode.BadRequest
    )
    { }
}