using System.Net;

namespace MediClubApp.Controllers.Base;

public abstract class ControllerBase
{
    protected async Task LayoutAsync(HttpListenerResponse response, string bodyHtml, string layoutName = "layout")
    {
        response.ContentType = "text/html";
        using var streamWriter = new StreamWriter(response.OutputStream);

        var html = (await File.ReadAllTextAsync($"html\\{layoutName}.html"))
            .Replace("{{body}}", bodyHtml);

        await streamWriter.WriteLineAsync(html);
        response.StatusCode = (int)HttpStatusCode.OK;
    }
    protected async Task WriteViewAsync(HttpListenerResponse response, string viewName, Dictionary<string, object>? viewValues = null, string? layoutName = null)
    {
        var html = await File.ReadAllTextAsync($"html\\{viewName}.html");

        if (viewValues is not null)
        {
            foreach (var viewValue in viewValues)
            {
                html = html.Replace("{{" + viewValue.Key + "}}", viewValue.Value.ToString());
            }
        }

        await LayoutAsync(response, html, layoutName ?? "layout");
    }
}
