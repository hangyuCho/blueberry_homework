using System.Net;

namespace BlueberryHomeworkApp.Middleware;

/// <summary>
/// 전역 예외 처리 미들웨어
/// 애플리케이션에서 발생하는 모든 예외를 중앙에서 처리합니다.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 미들웨어 생성자
    /// </summary>
    /// <param name="next">다음 미들웨어를 처리할 델리게이트</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 미들웨어 실행 메서드
    /// 모든 HTTP 요청에 대해 예외 처리를 수행합니다.
    /// </summary>
    /// <param name="context">HTTP 컨텍스트</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // 다음 미들웨어 실행
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            // 클라이언트 오류 (400 Bad Request)
            // 잘못된 인자나 유효하지 않은 요청에 대한 예외 처리
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(ex.Message);
        }
        catch (Exception ex)
        {
            // 서버 오류 (500 Internal Server Error)
            // 예상치 못한 서버 오류에 대한 예외 처리
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(ex.Message);
        }
    }
} 