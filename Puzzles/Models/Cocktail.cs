using Puzzles.Data;
using Puzzles.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puzzles.Models
{
    public class Cocktail:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public double Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        public DrinkCategory DrinkCategory { get; set; }

        // Relationships
        public List<Ingredient_Cocktail> Ingredients_Cocktails { get; set; }

        // Glass
        public int GlassId { get; set; }
        [ForeignKey("GlassId")]
        public Glass Glass {get; set; }

    }
}
