using MediClubApp.Services.Base;
namespace MediClubApp.Middleware;

public class LogMiddleware : IMiddleware
{
    public bool isLogging { get; set; }
    private readonly ILogService _logService;
    public LogMiddleware(ILogService logService, IConfiguration configuration)
    {
        _logService = logService;
        isLogging = configuration.GetSection("LogOptions:isLogging")?.Get<bool>() is null ? false : true;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next.Invoke(context: context);
    }
}
