using Puzzles.Data.Base;
using Puzzles.Data.ViewModels;
using Puzzles.Models;
using System.Threading.Tasks;

namespace Puzzles.Data.Services
{
    public interface ICocktailsService:IEntityBaseRepository<Cocktail>
    {
        Task<Cocktail> GetCocktailByIdAsync(int id);
        Task<NewCocktailDropdownVM> GetNewCocktailDropdownsValues();

        Task AddNewCocktailAsync(NewCocktailVM data);
        Task UpdateCocktailAsync(NewCocktailVM data);
    }
}
