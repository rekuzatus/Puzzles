using Microsoft.EntityFrameworkCore;
using Puzzles.Data.Base;
using Puzzles.Data.ViewModels;
using Puzzles.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puzzles.Data.Services
{
    public class CocktailsService : EntityBaseRepository<Cocktail>, ICocktailsService
    {
        private readonly AppDbContext _context;
        public CocktailsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewCocktailAsync(NewCocktailVM data)
        {
            var newCocktail = new Cocktail()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                DrinkCategory = data.DrinkCategory,
                GlassId = data.GlassId,
            };
            await _context.Cocktails.AddAsync(newCocktail);
            await _context.SaveChangesAsync();

            // Add Cocktail ingredients
            foreach (var ingredientId in data.IngredientIds)
            {
                var newIngredientCocktail = new Ingredient_Cocktail()
                {


                    CocktailId = newCocktail.Id,
                    IngredientId = ingredientId
                };
                await _context.Ingredients_Cocktails.AddAsync(newIngredientCocktail);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Cocktail> GetCocktailByIdAsync(int id)
        {
            var cocktailDetails = await _context.Cocktails
                .Include(g => g.Glass)
                .Include(ic => ic.Ingredients_Cocktails).ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            return cocktailDetails;
        }

        public async Task<List<Cocktail>> GetCocktailBySearch(string searchTerm)
        {
            var cocktailDetails = await _context.Cocktails
               .Include(ic => ic.Ingredients_Cocktails).ThenInclude(i => i.Ingredient)
               .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())
               || x.Ingredients_Cocktails.Any(n => n.Ingredient.Name.ToLower().Contains(searchTerm.ToLower()))).ToListAsync();

            return cocktailDetails;
        }

        public async Task<NewCocktailDropdownVM> GetNewCocktailDropdownsValues()
        {
            var response = new NewCocktailDropdownVM()
            {
                Ingredients = await _context.Ingredients.OrderBy(x => x.Name).ToListAsync(),
                Glasses = await _context.Glasses.OrderBy(x => x.Name).ToListAsync()
            };
            return response;
        }

        public async Task UpdateCocktailAsync(NewCocktailVM data)
        {
            var dbCocktail = await _context.Cocktails.FirstOrDefaultAsync(x => x.Id == data.Id);

            if (dbCocktail != null)           
            {
                dbCocktail.Name = data.Name;
                dbCocktail.Description = data.Description;
                dbCocktail.Price = data.Price;
                dbCocktail.ImageUrl = data.ImageUrl;
                dbCocktail.DrinkCategory = data.DrinkCategory;
                dbCocktail.GlassId = data.GlassId;
                await _context.SaveChangesAsync();
            };

            //Remove existing ingredients
            var existingIngredientsDb = _context.Ingredients_Cocktails.Where(x => x.CocktailId == data.Id).ToList();
            _context.Ingredients_Cocktails.RemoveRange(existingIngredientsDb);
            await _context.SaveChangesAsync();

            // Add Cocktail ingredients
            foreach (var ingredientId in data.IngredientIds)
            {
                var newIngredientCocktail = new Ingredient_Cocktail()
                {


                    CocktailId = data.Id,
                    IngredientId = ingredientId
                };
                await _context.Ingredients_Cocktails.AddAsync(newIngredientCocktail);
            }
            await _context.SaveChangesAsync();
        }
    }
}
