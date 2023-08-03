using YourLibrary.Models;

namespace YourLibrary.Abstractions;

public interface IRepository<TEntity> : 
    IRepositoryWriter<TEntity>, 
    IRepositoryReader<TEntity>, 
    IRepositoryDeleter<TEntity>,
    IRepositorySaver where TEntity : IEntity
{
    Task<bool> UpdateAsync(TEntity entity);
}
