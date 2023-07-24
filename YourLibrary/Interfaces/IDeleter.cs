using YourLibrary.Models;

namespace YourLibrary.Interfaces;

public interface IDeleter
{
    Task<bool> DeleteAsync(IEntity entity);
    Task<bool> DeleteAllAsync();
}