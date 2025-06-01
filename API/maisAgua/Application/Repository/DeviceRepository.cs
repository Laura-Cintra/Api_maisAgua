using maisAgua.Domain.Device;
using maisAgua.Domain.Exceptions;
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

        public async Task<List<Device>> GetAllAsync() => await _context.Devices.OrderBy(x => x.Id).Reverse().ToListAsync();

        public async Task<Device> GetByIdAsync(int id) => await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Device> AddAsync(Device entity)
        {
            try
            {
                await _context.Devices.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new DomainException("Falha ao adicionar dispositivo.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Device entity)
        {
            try
            {
                _context.Devices.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new DomainException("Falha ao deletar dispositivo.", ex);
            }
        }

        public async Task<Device> Update(Device entity)
        {
            try
            {
                _context.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new DomainException("Falha ao atualizar o dispositivo.", ex);
            }
        }
    }
}
