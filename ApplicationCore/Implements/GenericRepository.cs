﻿using System.Linq.Expressions;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ApplicationCore.Implements;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _dbContext = context;
        _dbSet = context.Set<T>();
    }
    
    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _dbSet;
        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).AsNoTracking().FirstOrDefaultAsync();

        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _dbSet;
        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).FirstOrDefaultAsync();

        return await query.AsNoTracking().Select(selector).FirstOrDefaultAsync();
    }

    public virtual async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).AsNoTracking().ToListAsync();

        return await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<ICollection<TResult>> GetListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null) query = include(query);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null) return await orderBy(query).AsNoTracking().Select(selector).ToListAsync();

        return await query.Select(selector).ToListAsync();
    }

    public virtual async Task<IPaginate<T>> GetPagingListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1,
        int size = 10)
    {
        IQueryable<T> query = _dbSet;
        if(include != null) query = include(query);
        if(predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).ToPaginateAsync(page, size, 1);
        return await query.AsNoTracking().ToPaginateAsync(page, size, 1);
    }

    public virtual async Task<IPaginate<TResult>> GetPagingListAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int page = 1, int size = 10)
    {
        IQueryable<T> query = _dbSet;
        if (include != null) query = include(query);
        if(predicate != null) query = query.Where(predicate);
        if (orderBy != null) return await orderBy(query).Select(selector).ToPaginateAsync(page, size, 1);
        return await query.AsNoTracking().Select(selector).ToPaginateAsync(page, size, 1);
    }

    public virtual async Task InsertAsync(T entity)
    {
        if (entity == null) return;
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task InsertRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual async void UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual async void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual async void DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async void DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}