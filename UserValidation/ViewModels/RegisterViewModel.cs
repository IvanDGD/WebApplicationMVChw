using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace UserValidation.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Surname is required")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "Incorrect phone number")]
        public string? PhoneNumber { get; set; }

        [Range(18, 100, ErrorMessage = "Age should from 18 to 100")]
        public int Age { get; set; }

        [StringLength(20, ErrorMessage = "Username should longer than 20 symbols")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username: only chars/nums/underline")]
        [Remote(action: "IsUsernameAvailable", controller: "Account", ErrorMessage = "Username is exited")]
        public string? Username { get; set; }

        [CreditCard(ErrorMessage = "Incorrect card number")]
        public string? CreditCardNumber { get; set; }

        [Url(ErrorMessage = "Неверный формат URL")]
        public string? Website { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "password length range is 6-100")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirm password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Confirm password length range is 6-100")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not compare")]
        public string ConfirmPassword { get; set; } = "";

        [ValidateNever]
        public bool TermsOfService { get; set; }
    }
}
