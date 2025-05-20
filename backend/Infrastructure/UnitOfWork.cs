using CrudVehiclesApp.Api.Domain.Entities;
using CrudVehiclesApp.Api.Domain.Interfaces;
using CrudVehiclesApp.Api.Infrastructure.Context;
using CrudVehiclesApp.Api.Infrastructure.Repositories;

namespace CrudVehiclesApp.Api.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IRepository<Vehicle>? _vehicleRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Vehicle> Vehicles
            => _vehicleRepository ??= new VehicleRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
