using BlueberryHomeworkApp.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Application;

public static class ApiResultExtensions
{
    public static IActionResult ToApiResult<T>(this IResult<T> result)
    {
        if (result.IsError)
        {
            return result.GetError() switch
            {
                DataNotFoundException _ => new NotFoundObjectResult(result.GetError()?.Message),
                ArgumentException _ => new BadRequestObjectResult(result.GetError()?.Message),
                _ => new ObjectResult(result.GetError()?.Message) { StatusCode = 500 }
            };
        }

        return new OkObjectResult(result.Get());
    }

    public static IActionResult ToApiResult(this IResult result)
    {
        if (result.IsError)
        {
            return new ObjectResult(result.GetError()?.Message) { StatusCode = 500 };
        }
        else
        {
            return new OkResult();
        }
    }
}