using Dongi.Data;
using Dongi.Services;
using Dongi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Dongi.Controllers;

[Authorize]
public class ExpenseController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IExpenseService _expenseService;

    public ExpenseController( ApplicationDbContext context, IExpenseService expenseService )
    {
        _context = context;
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<IActionResult> Create( int eventId )
    {
        var persons = await _context.EventPersons
            .Where(ep => ep.EventId == eventId)
            .Select(ep => ep.Person)
            .ToListAsync();

        var model = new CreateExpenseViewModel
        {
            EventId = eventId,
            Participants = persons.Select(p => new PersonSelectionItemViewModel
            {
                PersonId = p.Id,
                DisplayName = p.DisplayName
            }).ToList()
        };

        return View( model );
    }

    [HttpPost]
    public async Task<IActionResult> Create( CreateExpenseViewModel model )
    {
        if( !ModelState.IsValid )
            return View( model );

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _expenseService.AddExpenseAsync( model, userId! );

        return RedirectToAction( "Details", "Event", new { id = model.EventId } );
    }
}
