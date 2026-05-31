
namespace LogiSphere.Application.Interfaces;

public interface IRepository<TEntity>
{
    public Task AddAsync(TEntity entity);
    public Task<TEntity?> GetByIdAsync(Guid id);
}
