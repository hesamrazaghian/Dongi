using System.ComponentModel.DataAnnotations;

namespace Dongi.Models
{
    public class Event
    {
        public Event( )
        {
            EventPersons = new List<EventPerson>( );
            Expenses = new List<Expense>( );
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength( 200 )]
        public string Title { get; set; } = null!;

        [Required]
        public int CreatedByPersonId { get; set; }

        public Person CreatedBy { get; set; } = null!;

        public ICollection<EventPerson> EventPersons { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
