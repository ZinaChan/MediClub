using System.Text;
using MediClubApp.Models;
using MediClubApp.Services.Base;
using Microsoft.AspNetCore.Http.Extensions;
namespace MediClubApp.Middleware;

public class LogMiddleware : IMiddleware
{
    public bool isLogging { get; set; }
    private readonly ILogService _logService;
    public LogMiddleware(ILogService logService, IConfiguration configuration)
    {
        _logService = logService;
        isLogging = configuration.GetSection("LogOptions:isLogging")?.Get<bool>() ?? false;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!isLogging)
        {
            await next.Invoke(context: context);
            return;
        }

        var newLog = new Log();

        try
        {
            // Request Intialize
            this.InitializeLog(context: context, log: newLog);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

        await next.Invoke(context: context);

        try
        {
            // Response End/Finish
            this.FinishLog(context: context, log: newLog);
            await this._logService.CreateLogAsync(newLog: newLog);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    private async void InitializeLog(HttpContext context, Log log)
    {
        if (log is null)
        {
            throw new ArgumentNullException(nameof(log));
        }

        var request = context.Request;

        log.Url = request.GetDisplayUrl();
        log.CreationDate = DateTime.Now;

        request.EnableBuffering();
        var requestBody = await new StreamReader(request.Body, Encoding.UTF8).ReadToEndAsync();
        request.Body.Position = 0;

        log.RequestBody = requestBody;
    }

    private async void FinishLog(Log log, HttpContext context)
    {
        if (log is null)
        {
            throw new ArgumentNullException(nameof(log));
        }

        log.ResponsetBody = await this.LogResponseBody(context);

        log.StatusCode = context.Response.StatusCode;
        log.HttpMethod = context.Request.Method;
        log.EndDate = DateTime.Now;

    }

    private async Task<string> LogResponseBody(HttpContext context)
    {
        var response = context.Response;
        Stream responseBody = context.Response.Body;

        using var memoryStream = new MemoryStream();
        response.Body = memoryStream;

        memoryStream.Position = 0;
        var resultBody = await new StreamReader(memoryStream).ReadToEndAsync();

        memoryStream.Position = 0;
        await memoryStream.CopyToAsync(responseBody);
        response.Body = responseBody;

        return resultBody;
    }
}

