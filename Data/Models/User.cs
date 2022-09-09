using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; } = String.Empty;
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; } = String.Empty;
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<Collection> Collections { get; set; }

    }
}
