using System.Net;
using MediClubApp.Controllers.Base;
using MediClubApp.Repositories;
using MediClubApp.Extensions;

namespace MediClubApp.Controllers;

public class DoctorsController : ControllerBase
{

    private readonly DoctorSqlRepository _sqlRepository;
    public DoctorsController(DoctorSqlRepository sqlRepository)
    {
        _sqlRepository = sqlRepository;
    }
    public async Task Doctors(HttpListenerContext context)
    {
        var doctors = await _sqlRepository.GetAllDoctorsAsync();

        if (doctors is not null && doctors.Any())
        {
            var html = doctors.AsHtml();
            await base.LayoutAsync(context.Response, html);
        }
        else
        {
            new ErrorController().NotFound(context, nameof(doctors));
        }
    }
}
