using Microsoft.EntityFrameworkCore;
using RecipeOrganizatorMVC.Models;

namespace RecipeOrganizatorMVC.Common;

public class RecipeDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = now;
                ((BaseEntity)entity.Entity).Id = Guid.NewGuid();
            }

            ((BaseEntity)entity.Entity).UpdatedAt = now;
        }
    }

    public static void Seed(RecipeDbContext context)
    {
        if (!context.Recipes.Any())
        {
            context.Recipes.AddRange(
                new Recipe
                {
                    Name = "Spaghetti Carbonara",
                    Difficulty = RecipeDifficulty.Medium,
                    Cuisine = "Italian",
                    Ingredients = "Spaghetti, Eggs, Parmesan, Pancetta, Black Pepper",
                    Instructions = "Boil spaghetti. Fry pancetta. Mix eggs with cheese. Combine all ingredients.",
                    CookingTime = 20,
                    ImageUrl = "https://fakeimg.pl/1920x1080/?text=person%20image"
                },
                new Recipe
                {
                    Name = "Chicken Alfredo",
                    Difficulty = RecipeDifficulty.Medium,
                    Cuisine = "Italian",
                    Ingredients = "Chicken, Fettuccine, Cream, Parmesan, Garlic",
                    Instructions = "Cook chicken and pasta. Make the sauce by combining cream, garlic, and Parmesan.",
                    CookingTime = 25,
                    ImageUrl = "https://fakeimg.pl/1920x1080/?text=person%20image"
                },
                new Recipe
                {
                    Name = "Vegetable Stir Fry",
                    Difficulty = RecipeDifficulty.Easy,
                    Cuisine = "Chinese",
                    Ingredients = "Bell peppers, Carrots, Broccoli, Soy sauce, Garlic, Ginger",
                    Instructions = "Stir-fry vegetables with soy sauce, garlic, and ginger.",
                    CookingTime = 15,
                    ImageUrl = "https://fakeimg.pl/1920x1080/?text=person%20image"
                },
                new Recipe
                {
                    Name = "Beef Tacos",
                    Difficulty = RecipeDifficulty.Medium,
                    Cuisine = "Mexican",
                    Ingredients = "Ground beef, Taco shells, Lettuce, Tomato, Cheese, Salsa",
                    Instructions = "Cook the beef. Assemble tacos with desired toppings.",
                    CookingTime = 20,
                    ImageUrl = "https://fakeimg.pl/1920x1080/?text=person%20image"
                },
                new Recipe
                {
                    Name = "Caesar Salad",
                    Difficulty = RecipeDifficulty.Easy,
                    Cuisine = "American",
                    Ingredients = "Lettuce, Caesar dressing, Croutons, Parmesan",
                    Instructions = "Toss lettuce with Caesar dressing and top with croutons and Parmesan.",
                    CookingTime = 10,
                    ImageUrl = "https://fakeimg.pl/1920x1080/?text=person%20image"
                }
            );

            context.SaveChanges();
        }
    }
}