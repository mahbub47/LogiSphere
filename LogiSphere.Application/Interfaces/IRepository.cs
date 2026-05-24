
namespace LogiSphere.Application.Interfaces;

public interface IRepository<T>
{
    public Task AddAsync(T entity);
    public Task<T?> GetByIdAsync(Guid id);
}
