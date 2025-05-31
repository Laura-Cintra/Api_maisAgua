using maisAgua.Domain.Device;
using maisAgua.Domain.Interfaces;
using maisAgua.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace maisAgua.Application.Repository
{
    public class DeviceRepository : IRepository<Device>
    {

        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<List<Device>> getAllAsync() => await _context.Devices.ToListAsync();

        public async Task<Device> getByIdAsync(int id) => await _context.Devices.FirstOrDefaultAsync(d => d.Id == id);

        public async Task<Device> AddAsync(Device entity)
        {
            await _context.Devices.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Device entity)
        {
            _context.Devices.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Device> Update(Device entity)
        {
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
