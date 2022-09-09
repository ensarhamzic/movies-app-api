using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.ViewModels
{
    public class CollectionVM
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
