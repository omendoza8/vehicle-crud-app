using CrudVehiclesApp.Api.Application.DTOs;

namespace CrudVehiclesApp.Api.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetAllAsync();
        Task<VehicleDto?> GetByIdAsync(int id);
        Task<VehicleDto> AddAsync(VehicleDto dto);
        Task UpdateAsync(VehicleDto dto);
        Task DeleteAsync(int id);
        Task<VehicleDto?> GetByPlateAsync(string plate);
    }
}
