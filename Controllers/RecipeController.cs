using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizatorMVC.Common;
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
    public async Task<IActionResult> Create(Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(recipe.ImageUrl))
            {
                recipe.ImageUrl = $"https://via.placeholder.com/150?text={Uri.EscapeDataString(recipe.Name)}";
            }

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(recipe);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var recipe = _context.Recipes.Find(id);
        return View(recipe);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            var existingRecipe = _context.Recipes.Find(recipe.Id);
            if (existingRecipe == null)
            {
                return NotFound();
            }

            existingRecipe.Name = recipe.Name;
            existingRecipe.Description = recipe.Description;
            existingRecipe.Ingredients = recipe.Ingredients;
            existingRecipe.Instructions = recipe.Instructions;
            existingRecipe.Cuisine = recipe.Cuisine;
            existingRecipe.CookingTime = recipe.CookingTime;
            existingRecipe.Difficulty = recipe.Difficulty;
            existingRecipe.ImageUrl = recipe.ImageUrl;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(recipe);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(Guid id)
    {
        var recipe = _context.Recipes.Find(id);
        if (recipe == null)
        {
            return NotFound();
        }

        _context.Recipes.Remove(recipe);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }

        base.Dispose(disposing);
    }
}