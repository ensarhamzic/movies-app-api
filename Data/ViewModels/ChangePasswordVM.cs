using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.ViewModels
{
    public class ChangePasswordVM
    {
        public string CurrentPassword { get; set; }
        [MinLength(8)]
        public string NewPassword { get; set; }
        [MinLength(8)]
        public string RepeatPassword { get; set; }
    }
}
