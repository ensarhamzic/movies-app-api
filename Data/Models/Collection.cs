using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.Models
{
    public class Collection
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
