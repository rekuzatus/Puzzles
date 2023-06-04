using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Puzzles.Models;

namespace Puzzles.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient_Cocktail>().HasKey(ic => new
            {
                ic.IngredientId,
                ic.CocktailId
            });

            modelBuilder.Entity<Ingredient_Cocktail>().HasOne(c => c.Cocktail).WithMany(ic => ic.Ingredients_Cocktails).HasForeignKey(c => c.CocktailId);
            modelBuilder.Entity<Ingredient_Cocktail>().HasOne(c => c.Ingredient).WithMany(ic => ic.Ingredients_Cocktails).HasForeignKey(c => c.IngredientId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Glass> Glasses { get; set; }
        public DbSet<Ingredient_Cocktail> Ingredients_Cocktails { get; set; }
    }
}
