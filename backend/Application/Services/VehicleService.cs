using CrudVehiclesApp.Api.Application.DTOs;
using CrudVehiclesApp.Api.Application.Interfaces;
using CrudVehiclesApp.Api.Domain.Entities;
using CrudVehiclesApp.Api.Domain.Interfaces;

namespace CrudVehiclesApp.Api.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VehicleService> _logger; // Inyecta ILogger

        public VehicleService(IUnitOfWork unitOfWork, ILogger<VehicleService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            try
            {
                var vehicles = await _unitOfWork.Vehicles.GetAll();
                _logger.LogInformation("Retrieved {Count} vehicles.", vehicles.Count);
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
                _logger.LogError(ex, "An error occurred while retrieving all vehicles.");
                throw new InvalidOperationException("An error occurred while retrieving all vehicles.", ex);
            }
        }

        public async Task<VehicleDto?> GetByIdAsync(int id)
        {
            try
            {
                var vehicle = await _unitOfWork.Vehicles.GetById(id);
                if (vehicle == null) return null;
                _logger.LogInformation("Retrieved vehicle with ID {VehicleId}.", id);
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
                _logger.LogError(ex, "An error occurred while retrieving the vehicle with ID {VehicleId}.", id);
                throw new InvalidOperationException($"An error occurred while retrieving the vehicle with ID {id}.", ex);
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
                _logger.LogInformation("Added new vehicle with ID {VehicleId}.", vehicle.Id);
                return dto with { Id = vehicle.Id };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new vehicle.");
                throw new InvalidOperationException("An error occurred while adding a new vehicle.", ex);
            }
        }

        public async Task UpdateAsync(VehicleDto dto)
        {
            try
            {
                var vehicle = await _unitOfWork.Vehicles.GetById(dto.Id);
                if (vehicle == null)
                {
                    _logger.LogWarning("Vehicle with ID {VehicleId} not found for update.", dto.Id);
                    throw new KeyNotFoundException("Vehicle not found");
                }
                vehicle.Brand = dto.Brand;
                vehicle.Model = dto.Model;
                vehicle.Year = dto.Year;
                vehicle.LicensePlateNumber = dto.LicensePlateNumber;
                _unitOfWork.Vehicles.Update(vehicle);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Updated vehicle with ID {VehicleId}.", dto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the vehicle with ID {VehicleId}.", dto.Id);
                throw new InvalidOperationException($"An error occurred while updating the vehicle with ID {dto.Id}.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var vehicle = await _unitOfWork.Vehicles.GetById(id);
                if (vehicle == null)
                {
                    _logger.LogWarning("Vehicle with ID {VehicleId} not found for deletion.", id);
                    throw new KeyNotFoundException("Vehicle not found");
                }
                _unitOfWork.Vehicles.Delete(vehicle);
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("Deleted vehicle with ID {VehicleId}.", id);
            }
            catch (KeyNotFoundException)
            {
                throw; // Ya es claro y específico
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the vehicle with ID {VehicleId}.", id);
                throw new InvalidOperationException($"An error occurred while deleting the vehicle with ID {id}.", ex);
            }
        }

        public async Task<VehicleDto?> GetByPlateAsync(string plate)
        {
            try
            {
                var vehicles = await _unitOfWork.Vehicles.GetAll();
                var vehicle = vehicles.FirstOrDefault(v => v.LicensePlateNumber == plate);
                if (vehicle == null) return null;
                _logger.LogInformation("Retrieved vehicle with plate {Plate}.", plate);
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
                _logger.LogError(ex, "An error occurred while retrieving the vehicle with plate {Plate}.", plate);
                throw new InvalidOperationException($"An error occurred while retrieving the vehicle with plate {plate}.", ex);
            }
        }
    }
}
