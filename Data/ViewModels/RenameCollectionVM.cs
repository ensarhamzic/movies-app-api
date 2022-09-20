using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.ViewModels
{
    public class RenameCollectionVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
