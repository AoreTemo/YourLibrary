namespace YourLibrary.Abstractions;

public interface IRepositorySaver
{
    Task<bool> SaveAsync();
}