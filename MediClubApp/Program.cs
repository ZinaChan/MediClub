using System.Reflection;
using FluentValidation;
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
builder.Services.AddScoped<IDepartmentRepository,DepartmentEFCoreRepository>();
builder.Services.AddScoped<ISpecializationRepository,SpecializationEFCoreRepository>();
builder.Services.AddScoped<IMedicalRecordRepository,MedicalRecordEFCoreRepository>();
builder.Services.AddScoped<IAppointmentRepository,AppointmentEFCoreRepository>();
builder.Services.AddScoped<IRoomRepository,RoomEFCoreRepository>();
builder.Services.AddScoped<ILogRepository,LogEFCoreRRepository>();

builder.Services.AddScoped<IDoctorService,DoctorService>();
builder.Services.AddScoped<IPatientService,PatientService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<ISpecializationService,SpecializationService>();
builder.Services.AddScoped<IMedicalRecordService,MedicalRecordService>();
builder.Services.AddScoped<IAppointmentService,AppointmentService>();
builder.Services.AddScoped<IRoomService,RoomService>();
builder.Services.AddScoped<ILogService,LogService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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
