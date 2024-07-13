using MediClubApp.Models;
using MediClubApp.Repositories.Base;
using MediClubApp.Services.Base;

namespace MediClubApp.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        this._doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
    }
    public async Task CreateDoctorAsync(Doctor newDoctor)
    {
        if (newDoctor is null)
        {
            throw new ArgumentNullException(nameof(newDoctor));
        }

        await _doctorRepository.CreateAsync(newDoctor: newDoctor);
    }
    public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
    {
        return await _doctorRepository.GetAllAsync();
    }
    public async Task<Doctor?> GetDoctorAsync(Guid id)
    {
        return await this._doctorRepository.GetAsync(id: id);
    }
    public async Task UpdateDoctorAsync(Guid id, Doctor newDoctor)
    {

        if (newDoctor is null || await this._doctorRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(newDoctor));
        }

        await _doctorRepository.UpdateAsync(id: id, newDoctor: newDoctor);
    }
    public async Task DeleteDoctorByIdAsync(Guid id)
    {
        if (await this._doctorRepository.GetAsync(id) is null)
        {
            throw new ArgumentNullException(nameof(Doctor));
        }

        await _doctorRepository.DeleteByIdAsync(id: id);
    }

    public async Task<List<Doctor>> GetDoctorsByDepartmentAsync(Guid departmentId)
    {
       return await _doctorRepository.GetDoctorsByDepartmentAsync(departmentId: departmentId);
    }

    public async Task<List<Doctor>> GetDoctorsBySpecializationAsync(Guid specializationId)
    {

       return await _doctorRepository.GetDoctorsBySpecializationAsync(specializationId: specializationId);
    }
}
