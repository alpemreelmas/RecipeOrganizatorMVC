using System.ComponentModel.DataAnnotations;
using RecipeOrganizatorMVC.Common;

namespace RecipeOrganizatorMVC.Models;

public class Recipe : BaseEntity
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(250, MinimumLength = 1)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Ingredients are required")]
    public string? Ingredients { get; set; }

    [Required(ErrorMessage = "Instructions are required")]
    [MinLength(1, ErrorMessage = "Instructions must be at least 2 characters long")]
    public string? Instructions { get; set; }

    [Required(ErrorMessage = "Cuisine is required")]
    [StringLength(50)]
    public string Cuisine { get; set; }

    [Required(ErrorMessage = "Cooking time is required")]
    [Range(1, 240, ErrorMessage = "Cooking time must be between 1 and 240 minutes")]
    [Display(Name = "Cooking Time (minutes)")]
    public int CookingTime { get; set; }

    [Required]
    [Display(Name = "Difficulty Level")]
    public RecipeDifficulty Difficulty { get; set; }

    [Display(Name = "Image URL")]
    [Url(ErrorMessage = "Please enter a valid URL")]
    public string ImageUrl { get; set; }
}