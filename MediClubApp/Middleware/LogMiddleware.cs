
using MediClubApp.Services.Base;

namespace MediClubApp.Middleware;

public class LogMiddleware : IMiddleware
{
    private readonly ILogService _logService;
    public LogMiddleware(ILogService logService)
    {
        _logService = logService;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next.Invoke(context);
    }
}
