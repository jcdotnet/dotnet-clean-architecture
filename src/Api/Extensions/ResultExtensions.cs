using Domain.Abstractions;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ResultExtensions
{
    extension(Result result)
    {
        public ActionResult ToActionResult()
        {
            if (result.IsSuccess)
            {
                return new OkResult(); // maybe not needed
            }

            return result.Error.ErrorType switch
            {
                ErrorType.NotFound => new NotFoundObjectResult(result.Error),
                ErrorType.Validation => new BadRequestObjectResult(result.Error),
                ErrorType.BadRequest => new BadRequestObjectResult(result.Error),
                ErrorType.Unauthorized => new ObjectResult(result.Error) { StatusCode = 401 },
                _ => new ObjectResult(result.Error) { StatusCode = 500 }
            };
        }
    }
}



