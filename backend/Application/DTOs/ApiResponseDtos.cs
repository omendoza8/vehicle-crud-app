namespace CrudVehiclesApp.Api.Application.DTOs
{
    public record ApiResponseDto<T>(
            bool Success,
            string? Message,
            T? Data
        );
}
