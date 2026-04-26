using Dongi.Data;
using Dongi.Models;
using Dongi.Services;
using Dongi.ViewModels;

public class ExpenseService : IExpenseService
{
    private readonly ApplicationDbContext _context;
    private readonly IPersonService _personService;

    public ExpenseService( ApplicationDbContext context, IPersonService personService )
    {
        _context = context;
        _personService = personService;
    }

    public async Task AddExpenseAsync( CreateExpenseViewModel model, string userId )
    {
        var payer = await _personService.GetOrCreateCurrentPersonAsync(userId);

        var expense = new Expense
        {
            EventId = model.EventId,
            Title = model.Title,
            Amount = model.Amount,
            PaidByPersonId = payer.Id
        };

        _context.Expenses.Add( expense );
        await _context.SaveChangesAsync( );

        var selectedPersons = model.Participants
            .Where(p => p.IsSelected)
            .ToList();

        var share = model.Amount / selectedPersons.Count;

        foreach( var p in selectedPersons )
        {
            _context.ExpensePersons.Add( new ExpensePerson
            {
                ExpenseId = expense.Id,
                PersonId = p.PersonId,
                ShareAmount = share
            } );
        }

        await _context.SaveChangesAsync( );
    }
}
