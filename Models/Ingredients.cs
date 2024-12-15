using System.ComponentModel.DataAnnotations;
using Recipe_Organizer.Common;

namespace Recipe_Organizer.Models;

public class Ingredients : BaseEntity
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters long")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public string Quantity { get; set; }
    
    [Required]
    public Guid RecipeId { get; set; }

    public Recipe Recipe { get; set; }
}