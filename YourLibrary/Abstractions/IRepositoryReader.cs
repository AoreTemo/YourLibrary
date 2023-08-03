using YourLibrary.Models;

namespace YourLibrary.Abstractions;

public interface IRepositoryReader<TEntity> where TEntity : IEntity
{
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
}