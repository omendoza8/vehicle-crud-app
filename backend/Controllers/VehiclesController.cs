using CrudVehiclesApp.Api.Application.DTOs;
using CrudVehiclesApp.Api.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudVehiclesApp.Api.Controllers
{
    /// <summary>
    /// Manages vehicle operations such as retrieval, creation, update, and deletion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="vehicleService">Service for vehicle operations.</param>
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Retrieves all vehicles.
        /// </summary>
        /// <returns>A list of vehicles.</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<VehicleDto>>>> GetAll()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllAsync();
                if (vehicles == null || !vehicles.Any())
                {
                    return Ok(new ApiResponseDto<IEnumerable<VehicleDto>>(true, "No vehicles found.", vehicles));
                }
                return Ok(new ApiResponseDto<IEnumerable<VehicleDto>>(true, "Vehicles retrieved successfully.", vehicles));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponseDto<IEnumerable<VehicleDto>>(false, "An unexpected error occurred.", null));
            }
        }

        /// <summary>
        /// Retrieves a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The vehicle's unique identifier.</param>
        /// <returns>The vehicle with the specified ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<VehicleDto>>> GetById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetByIdAsync(id);
                if (vehicle == null)
                    return NotFound(new ApiResponseDto<VehicleDto>(false, "Vehicle not found.", null));

                return Ok(new ApiResponseDto<VehicleDto>(true, null, vehicle));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponseDto<VehicleDto>(false, "An unexpected error occurred.", null));
            }
        }

        /// <summary>
        /// Creates a new vehicle.
        /// </summary>
        /// <param name="dto">The vehicle data transfer object.</param>
        /// <returns>The created vehicle.</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<VehicleDto>>> Create([FromBody] VehicleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponseDto<VehicleDto>(false, "Invalid data.", null));

            try
            {
                var created = await _vehicleService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, new ApiResponseDto<VehicleDto>(true, "Vehicle created successfully.", created));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponseDto<VehicleDto>(false, "An unexpected error occurred.", null));
            }
        }

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="id">The vehicle's unique identifier.</param>
        /// <param name="dto">The updated vehicle data transfer object.</param>
        /// <returns>Status of the update operation.</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<VehicleDto>>> Update(int id, [FromBody] VehicleDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new ApiResponseDto<VehicleDto>(false, "ID mismatch.", null));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponseDto<VehicleDto>(false, "Invalid data.", null));

            try
            {
                await _vehicleService.UpdateAsync(dto);
                return Ok(new ApiResponseDto<VehicleDto>(true, "Vehicle updated successfully.", null));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponseDto<VehicleDto>(false, "Vehicle not found.", null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponseDto<VehicleDto>(false, "An unexpected error occurred.", null));
            }
        }

        /// <summary>
        /// Deletes a vehicle by its unique identifier.
        /// </summary>
        /// <param name="id">The vehicle's unique identifier.</param>
        /// <returns>Status of the delete operation.</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponseDto<VehicleDto>>> Delete(int id)
        {
            try
            {
                await _vehicleService.DeleteAsync(id);
                return Ok(new ApiResponseDto<VehicleDto>(true, "Vehicle deleted successfully.", null));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ApiResponseDto<VehicleDto>(false, "Vehicle not found.", null));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponseDto<VehicleDto>(false, "An unexpected error occurred.", null));
            }
        }

        /// <summary>
        /// Retrieves a vehicle by its license plate.
        /// </summary>
        /// <param name="plate">The vehicle's license plate.</param>
        /// <returns>The vehicle with the specified license plate.</returns>
        [HttpGet("plate/{plate}")]
        public async Task<ActionResult<ApiResponseDto<VehicleDto>>> GetByPlate(string plate)
        {
            try
            {
                var vehicle = await _vehicleService.GetByPlateAsync(plate);
                if (vehicle == null)
                    return NotFound(new ApiResponseDto<VehicleDto>(false, "Vehicle not found.", null));

                return Ok(new ApiResponseDto<VehicleDto>(true, null, vehicle));
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponseDto<VehicleDto>(false, "An unexpected error occurred.", null));
            }
        }
    }
}
