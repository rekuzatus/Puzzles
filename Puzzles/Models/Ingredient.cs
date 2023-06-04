using Puzzles.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Puzzles.Models
{
    public class Ingredient:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage ="An image is required")]
        public string ImageUrl { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "A name is required")]
        public string Name { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "A brand is required")]
        public string Brand { get; set; }

        // Relationships
        public List<Ingredient_Cocktail> Ingredients_Cocktails { get; set; }
    }
}
