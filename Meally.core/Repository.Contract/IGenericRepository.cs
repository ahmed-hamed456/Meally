using Meally.core.Specifications;

namespace Meally.core.Repository.Contract;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetEntityAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T?> GetEntityAsyncSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> GetAllAsyncSpec(ISpecification<T> spec);

    Task AddEntity(T entity);

    void UpdateEntity(T entity);
    void DeleteEntity(T entity);
}
