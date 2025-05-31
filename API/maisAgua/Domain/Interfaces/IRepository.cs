using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace maisAgua.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> getAllAsync();

    Task<T> getByIdAsync(int id);

    Task<T> AddAsync(T entity);

    Task<T> Update(T entity);

    Task<bool> Delete(T entity);
}