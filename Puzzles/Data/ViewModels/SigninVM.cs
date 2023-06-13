using System.ComponentModel.DataAnnotations;

namespace Puzzles.Data.ViewModels
{
    public class SigninVM
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&+=]).{8,}$", ErrorMessage = "Password must have at least 1 capital letter, 1 special character, 1 number, and be at least 8 characters long")]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
