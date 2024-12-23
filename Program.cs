using Microsoft.EntityFrameworkCore;
using RecipeOrganizatorMVC.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<RecipeDbContext>(options => options.UseSqlite("Data Source=recipes.sqlite"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    context.Database.Migrate();
    RecipeDbContext.Seed(context);
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Recipe}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();