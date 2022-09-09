using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.ViewModels
{
    public class UserLoginVM
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}
