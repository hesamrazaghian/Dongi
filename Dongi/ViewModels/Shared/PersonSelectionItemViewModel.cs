namespace Dongi.ViewModels;

public class PersonSelectionItemViewModel
{
    public int PersonId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}
