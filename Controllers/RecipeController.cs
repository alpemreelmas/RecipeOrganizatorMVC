using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe_Organizer.Common;
using Recipe_Organizer.Models;
using RecipeOrganizatorMVC.Models;

namespace RecipeOrganizatorMVC.Controllers;

public class RecipeController : Controller
{
    private readonly ILogger<RecipeController> _logger;
    private readonly RecipeDbContext _context;

    public RecipeController(ILogger<RecipeController> logger, RecipeDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Recipe> recipes = _context.Recipes
            .ToList();

        return View(recipes);
    }
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Recipe recipe, List<Ingredients> ingredients)
    {
        if (ModelState.IsValid)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(recipe);
    }

    public IActionResult Create()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}