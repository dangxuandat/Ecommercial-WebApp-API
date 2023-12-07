using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Implements;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext: DbContext
{
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(TContext context)
    {
        Context = context;
    }
    
    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        _repositories ??= new Dictionary<Type, object>();
        if (_repositories.TryGetValue(typeof(TEntity), out object repository))
        {
            return (IGenericRepository<TEntity>)repository;
        }

        repository = new GenericRepository<TEntity>(Context);
        _repositories.Add(typeof(TEntity), repository);
        return (IGenericRepository<TEntity>)repository;
    }

    public void Dispose()
    {
        Context?.Dispose();
    }

    public int Commit()
    {
        return Context.SaveChanges();
    }

    public Task<int> CommitAsync()
    {
        return Context.SaveChangesAsync();
    }

    public TContext Context { get; }
}