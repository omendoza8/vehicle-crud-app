using CrudVehiclesApp.Api.Domain.Entities;
using CrudVehiclesApp.Api.Domain.Interfaces;
using CrudVehiclesApp.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CrudVehiclesApp.Api.Infrastructure.Repositories
{
    public class VehicleRepository : IRepository<Vehicle>
    {
        private readonly AppDbContext _context;
        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Vehicle>> GetAll()
        {
            return await _context.Vehicles.AsNoTracking().ToListAsync();
        }
        public async Task<Vehicle?> GetById(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }
        public void Add(Vehicle entity)
        {
            _context.Vehicles.Add(entity);
        }
        public void Update(Vehicle entity)
        {
            _context.Vehicles.Update(entity);
        }
        public void Delete(Vehicle entity)
        {
            _context.Vehicles.Remove(entity);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
