using System.Net;
using MediClubApp.Controllers.Base;
using MediClubApp.Repositories;
using MediClubApp.Extensions;

namespace MediClubApp.Controllers;

public class PatientController : ControllerBase
{
    private readonly PatientSqlRepository _sqlRepository;
    public PatientController(PatientSqlRepository sqlRepository)
    {
        _sqlRepository = sqlRepository;
    }
    public async Task Patients(HttpListenerContext context)
    {
        var patients = await _sqlRepository.GetAllPatientsAsync();

        if (patients is not null && patients.Any())
        {
            var html = patients.AsHtml();
            await base.LayoutAsync(context.Response, html);
        }
        else
        {
            new ErrorController().NotFound(context, nameof(patients));
        }
    }
}
