#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategories.Models
{
public class Category
{    
    [Key]    

    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "is required")] 
    [Display(Name ="Name")]
    public string Name { get; set; }
    
    
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<Relation> Relationships  {get; set;} = new List<Relation>();
}
}




