using System;
using ecommerceApi.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.DAL.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetListAsync();

    Task<T> GetByGidAsync(Guid gid);

    Task<T> AddAsync(T entity);

    Task<bool> DeleteAsync(T entity);

    Task<T> UpdateAsync(T entity);
}

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly EcommerceContext _ecommerceContext;

    internal DbSet<T> _dbSet;

    public Repository(EcommerceContext ecommerceContext)
    {
        _ecommerceContext = ecommerceContext;
        this._dbSet = _ecommerceContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetListAsync()
        => await _dbSet.AsNoTracking().ToListAsync<T>();

    public async Task<T> GetByGidAsync(Guid gid)
        => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Gid == gid);

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _ecommerceContext.SaveChangesAsync(); 

        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        await _dbSet
            .Where(x => x.Gid == entity.Gid)
            .ExecuteDeleteAsync();

        await _ecommerceContext.SaveChangesAsync();

        return true;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _ecommerceContext.Update(entity);
        await _ecommerceContext.SaveChangesAsync();

        return entity;
    }
}