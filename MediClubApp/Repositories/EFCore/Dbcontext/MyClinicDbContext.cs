using MediClubApp.Models;
using MediClubApp.Options;
using MediClubApp.Options.Base;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace MediClubApp.Repositories.EFCore.Dbcontext;

public class MyClinicDbContext : DbContext
{
    private readonly IConnectionStringOption _connectionStringOption;

    public MyClinicDbContext(IOptionsSnapshot<MsSqlConnectionOption> sqlOptions)
    {
        this._connectionStringOption = sqlOptions.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(connectionString: _connectionStringOption.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Log> Logs { get; set; }
}
