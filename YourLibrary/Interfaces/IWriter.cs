using YourLibrary.Models;

namespace YourLibrary.Interfaces;

public interface IWriter
{
    Task<bool> AddAsync(IEntity entity);
    Task<bool> AddRangeAsync(IEnumerable<IEntity> entity);
    
}