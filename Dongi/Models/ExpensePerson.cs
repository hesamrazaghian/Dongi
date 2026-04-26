using System.ComponentModel.DataAnnotations;

namespace Dongi.Models
{
    public class ExpensePerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExpenseId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int ShareAmount { get; set; }

        public Expense Expense { get; set; } = null!;
        public Person Person { get; set; } = null!;
    }
}
