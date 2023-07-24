using YourLibrary.Models;

namespace YourLibrary.Abstractions;

public interface IRepository<TEntity> where TEntity : IEntity
{
    Task<bool> AddAsync(TEntity entity);
    Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task<bool> DeleteAllAsync();
}
