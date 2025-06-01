using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace maisAgua.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task<T> AddAsync(T entity);

    Task<T> Update(T entity);

    Task<bool> DeleteAsync(T entity);
}