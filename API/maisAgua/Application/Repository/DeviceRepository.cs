using maisAgua.Domain.Exceptions;
using maisAgua.Domain.Interfaces;
using maisAgua.Domain.Persistence.Devices;
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

        public async Task<List<Device>> GetAllAsync()
        {
            try
            {
                return await _context.Devices.OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de buscar dispositivos cancelada.", ex);
            }
        }

        public async Task<Device> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de buscar dispositivo por id cancelada.", ex);
            }
        }

        public async Task<Device> AddAsync(Device entity)
        {
            try
            {
                await _context.Devices.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DomainException("Falha criar dispositivo no banco de dados. NOME JÁ EXISTE. " + ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de adicionar dispositivo no banco de dados cancelada.", ex);
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
            catch (DbUpdateException ex)
            {
                throw new DomainException("Falha ao deletar dispositivo no banco de dados.", ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de deletar dispositivo no banco de dados cancelada.", ex);
            }
        }

        public async Task<Device> UpdateAsync(Device entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DomainException("Falha ao atualizar o dispositivo no banco de dados.", ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de deletar dispositivo no banco de dados cancelada.", ex);
            }
        }
    }
}
