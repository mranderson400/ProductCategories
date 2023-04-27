#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategories.Models;

public class Product

{
    [Key]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Name")]
    public string Name { get; set; }
    public int Price { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "must be at least 3 chars")]
    [MaxLength(45, ErrorMessage = "cant be more than 45 chars")]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //foreignnkey here ?
    public List<Relation> Relationships { get; set; } = new List<Relation>();

}
