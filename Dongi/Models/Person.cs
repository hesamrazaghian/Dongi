using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Dongi.Models
{
    public class Person
    {
        public Person( )
        {
            EventPersons = new List<EventPerson>( );
            ExpensesPaid = new List<Expense>( );
            ExpensePersons = new List<ExpensePerson>( );
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [MaxLength( 100 )]
        public string DisplayName { get; set; } = null!;

        public IdentityUser User { get; set; } = null!;

        public ICollection<EventPerson> EventPersons { get; set; }
        public ICollection<Expense> ExpensesPaid { get; set; }
        public ICollection<ExpensePerson> ExpensePersons { get; set; }
    }
}
