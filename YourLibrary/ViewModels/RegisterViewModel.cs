using System.ComponentModel.DataAnnotations;

namespace YourLibrary.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email address")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string  Password { get; set; }
    
    [Required(ErrorMessage = "Password must be confirmed")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password don't match")]
    public string ConfirmPassword { get; set; }
}