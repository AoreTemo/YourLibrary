using YourLibrary.Models;

namespace YourLibrary.Interfaces;

public interface IReader
{
    Task<List<IEntity>> GetAllAsync();
    Task<IEntity> GetByIdAsync(int id);
    Task<IEntity> GetByNameAsync(string name);
}