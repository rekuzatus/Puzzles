using Puzzles.Models;
using System.Collections.Generic;

namespace Puzzles.Data.ViewModels
{
    public class NewCocktailDropdownVM
    {
        public NewCocktailDropdownVM()
        {
            Glasses = new List<Glass>();
            Ingredients= new List<Ingredient>();
        }
        public List<Glass> Glasses { get; set; }
        public List<Ingredient> Ingredients { get; set; }

    }
}
