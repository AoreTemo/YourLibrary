using YourLibrary.Models;

namespace YourLibrary.ViewModels;

public class CreateBookViewModel
{
    public string Name { get; set; }
    
    public Author Author { get; set; }
    
    public IFormFile? Image { get; set; }
}