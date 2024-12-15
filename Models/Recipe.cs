using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Recipe_Organizer.Common;

namespace Recipe_Organizer.Models;

public class Recipe : BaseEntity
{
    [Required (ErrorMessage = "Name is required")]
    [StringLength(250, MinimumLength = 1)]
    public string Name { get; set; }

    public string? Description { get; set; }
    
    [Required (ErrorMessage = "Ingredients are required")]
    public List<Ingredients> Ingredients { get; set; }
    
    [Required (ErrorMessage = "Instructions are required")]
    [MinLength(1, ErrorMessage = "Instructions must be at least 2 characters long")]
    public string Instructions { get; set; }
    
    [Required (ErrorMessage = "Preparing time is required")]
    [Range(1, 1000, ErrorMessage = "Preparing time must be between 1 and 1000 minutes")]
    public int PreparingTime { get; set; }
}

