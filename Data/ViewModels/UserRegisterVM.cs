using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.ViewModels
{
    public class UserRegisterVM : IValidatableObject
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string ConfirmPassword { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult("Passwords don't match");
            }
        }
    }
}
