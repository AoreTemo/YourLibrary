using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourLibrary.Models;
public class Book : IEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }
    public Author Author { get; set; }

    [ForeignKey("Image")]
    public int? ImageId { get; set; }
    public Image? Image { get; set; }
}


