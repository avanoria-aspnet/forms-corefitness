namespace Domain.Abstractions.Repositories;

public interface IRepositoryBase<TModel, TId>
{
    Task AddAsync(TModel model, CancellationToken ct = default);
    Task<bool> UpdateAsync(TModel model, CancellationToken ct = default);
    Task<bool> DeleteAsync(TModel model, CancellationToken ct = default);
    Task<bool> DeleteByIdAsync(TId id, CancellationToken ct = default);

    Task<TModel?> GetByIdAsync(TId id, CancellationToken ct = default);
    Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken ct = default);
}
