using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourLibrary.Models;

public class Image
{
    [Key] public int Id { get; set; }
    public string Link { get; set; }
}
