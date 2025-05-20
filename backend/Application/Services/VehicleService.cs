using CrudVehiclesApp.Api.Application.DTOs;
using CrudVehiclesApp.Api.Application.Interfaces;
using CrudVehiclesApp.Api.Domain.Entities;
using CrudVehiclesApp.Api.Domain.Interfaces;

namespace CrudVehiclesApp.Api.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            try
            {
                var vehicles = await _unitOfWork.Vehicles.GetAll();
                return vehicles.Select(v => new VehicleDto(
                    v.Id,
                    v.Brand,
                    v.Model,
                    v.Year,
                    v.LicensePlateNumber
                ));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all vehicles.", ex);
            }
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            try
            {
                var vehicle = await _unitOfWork.Vehicles.GetById(id);
                if (vehicle == null) return null;
                return new VehicleDto(
                    vehicle.Id,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.Year,
                    vehicle.LicensePlateNumber
                );
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the vehicle with ID {id}.", ex);
            }
        }

        public async Task<VehicleDto> AddAsync(VehicleDto dto)
        {
            try
            {
                var vehicle = new Vehicle
                {
                    Brand = dto.Brand,
                    Model = dto.Model,
                    Year = dto.Year,
                    LicensePlateNumber = dto.LicensePlateNumber
                };
                _unitOfWork.Vehicles.Add(vehicle);
                await _unitOfWork.SaveChangesAsync();
                return dto with { Id = vehicle.Id };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new vehicle.", ex);
            }
        }

        public async Task UpdateAsync(VehicleDto dto)
        {
            try
            {
                var vehicle = await _unitOfWork.Vehicles.GetById(dto.Id);
                if (vehicle == null) throw new KeyNotFoundException("Vehicle not found");
                vehicle.Brand = dto.Brand;
                vehicle.Model = dto.Model;
                vehicle.Year = dto.Year;
                vehicle.LicensePlateNumber = dto.LicensePlateNumber;
                _unitOfWork.Vehicles.Update(vehicle);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the vehicle with ID {dto.Id}.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var vehicle = await _unitOfWork.Vehicles.GetById(id);
                if (vehicle == null) throw new KeyNotFoundException("Vehicle not found");
                _unitOfWork.Vehicles.Delete(vehicle);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the vehicle with ID {id}.", ex);
            }
        }

        public async Task<VehicleDto?> GetByPlateAsync(string plate)
        {
            try
            {
                var vehicles = await _unitOfWork.Vehicles.GetAll();
                var vehicle = vehicles.FirstOrDefault(v => v.LicensePlateNumber == plate);
                if (vehicle == null) return null;
                return new VehicleDto(
                    vehicle.Id,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.Year,
                    vehicle.LicensePlateNumber
                );
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the vehicle with plate {plate}.", ex);
            }
        }
    }
}
