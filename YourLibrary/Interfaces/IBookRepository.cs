using YourLibrary.Models;

namespace YourLibrary.Interfaces;

public interface IRepository
{
    Task<bool> SaveAsync();
}