using System.ComponentModel.DataAnnotations;

namespace Dongi.ViewModels
{
    public class CreateExpenseViewModel
    {
        public int EventId { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Amount { get; set; }

        public List<PersonSelectionItemViewModel> Participants { get; set; } = new( );
    }
}