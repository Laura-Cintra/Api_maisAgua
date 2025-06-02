using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace maisAgua.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> AddAsync(T entity);

    Task<T> GetByIdAsync(int id);

    Task<T> UpdateAsync(T entity);

    Task<bool> DeleteAsync(T entity);
}