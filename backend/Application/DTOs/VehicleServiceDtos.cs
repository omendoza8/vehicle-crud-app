namespace CrudVehiclesApp.Api.Application.DTOs
{
    public record VehicleDto
        (
            int Id,
            string Brand,
            string Model,
            DateTime Year,
            string LicensePlateNumber
        );
}
