using Domain.Abstractions;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        public ActionResult ToActionResult() => result switch
        {
            T value => new OkObjectResult(value),

            Error error => MapError(error),

            null => MapError(Error.NullValue)
        };
    }
    private static ActionResult MapError(Error error) => error.ErrorType switch
    {
        ErrorType.NotFound => new NotFoundObjectResult(error),
        ErrorType.Validation => new BadRequestObjectResult(error),
        ErrorType.BadRequest => new BadRequestObjectResult(error),
        ErrorType.Unauthorized => new ObjectResult(error) { StatusCode = 401 },
        _ => new ObjectResult(error) { StatusCode = 500 }
    };
}



