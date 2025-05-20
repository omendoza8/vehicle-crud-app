using CrudVehiclesApp.Api.Domain.Entities;

namespace CrudVehiclesApp.Api.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Vehicle> Vehicles { get; }
        Task<int> SaveChangesAsync();
    }
}
