using MediClubApp.Middleware;
using MediClubApp.Options;
using MediClubApp.Repositories.Base;
using MediClubApp.Repositories.Dapper;
using MediClubApp.Repositories.EFCore;
using MediClubApp.Repositories.EFCore.Dbcontext;
using MediClubApp.Services;
using MediClubApp.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyClinicDbContext>();

builder.Services.AddScoped<IDoctorRepository,DoctorEFCoreRepository>();
builder.Services.AddScoped<IPatientRepository,PatientEFCoreRRepository>();
builder.Services.AddScoped<ILogRepository,LogEFCoreRRepository>();

builder.Services.AddScoped<IDoctorService,DoctorService>();
builder.Services.AddScoped<IPatientService,PatientService>();
builder.Services.AddScoped<ILogService,LogService>();

builder.Services.AddScoped<LogMiddleware>();

var connectionStringSection = builder.Configuration.GetSection("Connections:MediClubDb");
builder.Services.Configure<MsSqlConnectionOption>(config: connectionStringSection);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<LogMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
