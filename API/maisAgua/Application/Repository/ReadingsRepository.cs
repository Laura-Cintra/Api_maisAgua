using maisAgua.Domain.Exceptions;
using maisAgua.Domain.Interfaces;
using maisAgua.Domain.Persistence.Readings;
using maisAgua.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace maisAgua.Application.Repository
{
    public class ReadingsRepository : IRepository<Reading>
    {
        private readonly AppDbContext _context;

        public ReadingsRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Reading>> GetAllAsync()
        {
            try
            {
                return await _context.Readings.OrderByDescending(x => x.Id).ToListAsync();
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de buscar leituras cancelada.", ex);
            }
        }
        public async Task<Reading> AddAsync(Reading entity)
        {
            try
            {
                await _context.Readings.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DomainException("Erro ao adicionar leitura no banco de dados.", ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de adicionar leitura no banco de dados cancelada.", ex);
            }
        }

        public async Task<Reading> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Readings.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de buscar leitura por id cancelada.", ex);
            }
        }

        public async Task<Reading> UpdateAsync(Reading entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException ex)
            {
                throw new DomainException("Erro ao atualizar leitura no banco de dados.", ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de atualizar leitura no banco de dados cancelada.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Reading entity)
        {
            try
            {
                _context.Readings.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DomainException("Erro ao remover leitura do banco de dados.", ex);
            }
            catch (OperationCanceledException ex)
            {
                throw new DomainException("Operação de remover leitura do banco de dados cancelada.", ex);
            }
        }
    }
}
