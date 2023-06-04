using Puzzles.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Puzzles.Models
{
    public class Glass:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name= "Name")]
        [Required(ErrorMessage = "A name is required")]
        public string Name { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "An image is required")]
        public string ImageUrl { get; set; }

        // Relationships
        public List<Cocktail> Cocktails { get; set; }
    }
}
