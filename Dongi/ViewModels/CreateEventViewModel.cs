using System.ComponentModel.DataAnnotations;

namespace Dongi.ViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        [StringLength( 100 )]
        public string Title { get; set; } = string.Empty;

        [StringLength( 500 )]
        public string? Description { get; set; }
    }
}
