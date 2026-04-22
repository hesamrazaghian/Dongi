using System.ComponentModel.DataAnnotations;

namespace Dongi.Models
{
    public class EventPerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Event Event { get; set; } = null!;
        public Person Person { get; set; } = null!;
    }
}
