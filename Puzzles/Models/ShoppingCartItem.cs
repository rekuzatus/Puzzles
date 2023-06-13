using System.ComponentModel.DataAnnotations;

namespace Puzzles.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Cocktail Cocktail { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
