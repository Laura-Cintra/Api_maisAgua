using maisAgua.Domain.Device;
using maisAgua.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace maisAgua.Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Reading> Readings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceMapping());
            modelBuilder.ApplyConfiguration(new ReadingMapping());
        }
    }
}
