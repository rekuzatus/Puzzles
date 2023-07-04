using System;
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
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [MinimumAge(18, ErrorMessage = "You must be at least 18 years old")]
        [Range(typeof(DateTime), "01/01/1900", "01/31/2023", ErrorMessage = "Please enter a valid date.")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&+=]).{8,}$", ErrorMessage = "Password must have at least 1 capital letter, 1 special character, 1 number, and be at least 8 characters long")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }

    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                if (DateTime.Today.AddYears(-_minimumAge) < dateOfBirth)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

        public override bool IsValid(object value)
        {
            // Handle nullable DateTime (e.g., DateTime?)
            if (value == null)
            {
                return true;
            }

            return base.IsValid(value);
        }
    }
}
