using System.ComponentModel.DataAnnotations;

namespace Movies_API.Data.ViewModels
{
    public class DeleteCollectionVM
    {
        [Required]
        public int Id { get; set; }
    }
}
