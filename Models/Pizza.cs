using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models;

public class Pizza
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Pizza name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
    
    [Required]
    [Range(0.01, 999.99, ErrorMessage = "Price must be between $0.01 and $999.99")]
    public decimal Price { get; set; }
    
    public bool IsGlutenFree { get; set; }
    
    [Required(ErrorMessage = "Size is required")]
    public PizzaSize Size { get; set; }
    
    public List<string>? Toppings { get; set; }
    
    [Url(ErrorMessage = "Invalid image URL format")]
    public string? ImageUrl { get; set; }
}

public enum PizzaSize
{
    Small = 0,
    Medium = 1,
    Large = 2,
    ExtraLarge = 3
}