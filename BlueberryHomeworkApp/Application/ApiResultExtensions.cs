using System.Security.Authentication;
using BlueberryHomeworkApp.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Application;

public static class ApiResultExtensions
{
    public static IActionResult ToApiResult<T>(this IResult<T> result)
    {
        return result.IsError
            ? GetError(result)
            : new OkObjectResult(result.Get());
    }

    public static IActionResult ToApiResult(this IResult result)
    {
        return result.IsError
            ? GetError(result)
            : new OkResult();
    }

    private static IActionResult GetError(this IResult result) =>
        result.GetError() switch
        {
            DataNotFoundException _ => new NotFoundObjectResult(result.GetError()?.Message),
            ArgumentException _ => new BadRequestObjectResult(result.GetError()?.Message),
            ConflictException ex => new ConflictObjectResult(ex.Message),
            AuthenticationException _ => new UnauthorizedObjectResult(result.GetError()?.Message),
            _ => new ObjectResult(result.GetError()?.Message) { StatusCode = 500 }
        };
}