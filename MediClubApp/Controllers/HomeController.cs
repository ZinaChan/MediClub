using System.Net;
using MediClubApp.Controllers.Base;

namespace MediClubApp.Controllers;

public class HomeController : ControllerBase
{
    public async Task Home(HttpListenerContext context)
    {
        await base.WriteViewAsync(context.Response, "index");
    }

}
