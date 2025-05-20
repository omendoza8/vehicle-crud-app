using CrudVehiclesApp.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudVehiclesApp.Api.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }

    }
}
