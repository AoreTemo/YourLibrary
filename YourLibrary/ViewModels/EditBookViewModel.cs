using YourLibrary.Models;

namespace YourLibrary.ViewModels;

public class EditBookViewModel
{
    public string Name { get; set; }
    
    public string Author { get; set; }
    
    public IFormFile? Image { get; set; }
}
