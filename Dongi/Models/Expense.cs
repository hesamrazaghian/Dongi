using System.ComponentModel.DataAnnotations;

namespace Dongi.Models
{
    public class Expense
    {
        public Expense( )
        {
            ExpensePersons = new List<ExpensePerson>( );
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength( 200 )]
        public string Title { get; set; } = null!;

        public int Amount { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int PaidByPersonId { get; set; }

        public Event Event { get; set; } = null!;
        public Person PaidBy { get; set; } = null!;

        public ICollection<ExpensePerson> ExpensePersons { get; set; }
    }
}
