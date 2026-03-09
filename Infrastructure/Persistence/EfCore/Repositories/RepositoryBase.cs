using Domain.Abstractions.Repositories;
using Domain.Exceptions.Custom;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Persistence.EfCore.Repositories;

public abstract class RepositoryBase<TModel, TId, TEntity, TContext>(TContext context) : IRepositoryBase<TModel, TId> where TEntity : class where TContext : DbContext
{
    protected readonly TContext _context = context;
    protected DbSet<TEntity> Set => _context.Set<TEntity>();

    protected abstract TId GetId(TModel model);
    protected abstract void ApplyUpdates(TModel model, TEntity entity);
    protected abstract TModel ToModel(TEntity entity);
    protected abstract TEntity ToEntity(TModel model);

    public virtual async Task AddAsync(TModel model, CancellationToken ct = default)
    {
        try
        {
            if (model is null)
                throw new NullDomainException("Model must be provided.");

            var entity = ToEntity(model);

            await Set.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);
            throw;
        }
    }

    public virtual async Task<bool> UpdateAsync(TModel model, CancellationToken ct = default)
    {
        try
        {
            if (model is null)
                throw new NullDomainException("Model must be provided.");

            var id = GetId(model);

            var entity = await Set.FindAsync([id], ct);
            if (entity is null)
                return false;

            ApplyUpdates(model, entity);
            await _context.SaveChangesAsync(ct);

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);
            throw;
        }
    }

    public virtual async Task<bool> DeleteAsync(TModel model, CancellationToken ct = default)
    {
        try
        {
            if (model is null)
                throw new NullDomainException("Model must be provided.");

            var id = GetId(model);

            var entity = await Set.FindAsync([id], ct);
            if (entity is null)
                return false;

            Set.Remove(entity);
            await _context.SaveChangesAsync(ct);

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);
            throw;
        }
    }

    public virtual async Task<bool> DeleteByIdAsync(TId id, CancellationToken ct = default)
    {
        try
        {
            var entity = await Set.FindAsync([id], ct);
            if (entity is null)
                return false;

            Set.Remove(entity);
            await _context.SaveChangesAsync(ct);

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);
            throw;
        }
    }

    public virtual async Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default)
    {
        try
        {
            var entity = await Set.FindAsync([id], ct);
            return entity is null ? default : ToModel(entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);
            throw;
        }
    }

    public virtual async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default)
    {
        try
        {
            var entities = await Set.AsNoTracking().ToListAsync(ct);
            return [.. entities.Select(ToModel)];
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            Console.WriteLine(ex);
            throw;
        }
    }
}
