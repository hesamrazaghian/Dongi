using Dongi.Data;
using Dongi.Models;
using Microsoft.EntityFrameworkCore;

public class PersonService : IPersonService
{
    private readonly ApplicationDbContext _context;

    public PersonService( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<Person> GetOrCreateCurrentPersonAsync( string userId )
    {
        var person = await _context.Persons
        .FirstOrDefaultAsync(p => p.UserId == userId);

        if( person != null )
            return person;

        person = new Person
        {
            UserId = userId,
            DisplayName = "User Display Name"
        };

        _context.Persons.Add( person );
        await _context.SaveChangesAsync( );

        return person;
    }
}
