using Puzzles.Data;
using Puzzles.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puzzles.Models
{
    public class NewCocktailVM
    {
        public int Id { get; set; }

        [Display(Name = "Cocktail name")]
        [Required(ErrorMessage = "Name is required")]    
        public string Name { get; set; }

        [Display(Name = "Cocktail description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Cocktail price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Cocktail image")]
        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Cocktail category is required")]
        public DrinkCategory DrinkCategory { get; set; }

        // Relationships
        [Display(Name = "Select ingredients")]
        [Required(ErrorMessage = "Cocktail ingredients are required")]
        public List<int> IngredientIds { get; set; }

        // Glass
        [Display(Name = "Select a glass")]
        [Required(ErrorMessage = "Glass is required")]
        public int GlassId { get; set; }

    }
}
