using YourLibrary.Models;

namespace YourLibrary.Abstractions;

public interface IRepositoryDeleter<TEntity> where TEntity : IEntity
{
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task<bool> DeleteAllAsync();
}