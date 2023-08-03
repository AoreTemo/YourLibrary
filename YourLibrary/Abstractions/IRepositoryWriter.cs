using YourLibrary.Models;

namespace YourLibrary.Abstractions;

public interface IRepositoryWriter<TEntity> where TEntity : IEntity
{
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(IEnumerable<TEntity>entities);
}