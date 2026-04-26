using Dongi.ViewModels;

public interface IExpenseService
{
    Task AddExpenseAsync( CreateExpenseViewModel model, string userId );
}
