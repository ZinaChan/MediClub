using System.Net;
using MediClubApp.Controllers.Base;

namespace MediClubApp.Controllers;

public class ErrorController : ControllerBase
{
  public async void NotFound(HttpListenerContext context, string resourceName)
  {
    Dictionary<string, object>? viewValues = new() {
        {"resource", resourceName ?? "/"}
      };

    await WriteViewAsync(context.Response, "notfound", viewValues);
  }
}
