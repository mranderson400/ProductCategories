#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategories.Models;
public class Relation
{    
    [Key]    

    public int RelationId { get; set; }
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;







    public int ProductId {get; set;}
    public Product? Product {get; set;}
    public int CategoryId {get; set;}
    public Category? Category  {get; set;}
}




