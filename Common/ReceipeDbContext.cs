using Microsoft.EntityFrameworkCore;
using RecipeOrganizatorMVC.Models;

namespace RecipeOrganizatorMVC.Common;

public class RecipeDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recipe>().ToTable("Recipes");
    }

    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
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
}