using Dongi.Data;
using Dongi.Models;
using Microsoft.EntityFrameworkCore;

namespace Dongi.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPersonService _personService;

        public EventService( ApplicationDbContext context, IPersonService personService )
        {
            _context = context;
            _personService = personService;
        }

        public async Task<int> CreateEventAsync( string title, string? description, string userId )
        {
            var person = await _personService.GetOrCreateCurrentPersonAsync(userId);

            var ev = new Event
            {
                Title = title,
                CreatedByPersonId = person.Id
            };

            _context.Events.Add( ev );
            await _context.SaveChangesAsync( );

            var eventPerson = new EventPerson
            {
                EventId = ev.Id,
                PersonId = person.Id
            };

            _context.EventPersons.Add( eventPerson );
            await _context.SaveChangesAsync( );

            return ev.Id;
        }

        public async Task<List<Event>> GetMyEventsAsync( string userId )
        {
            var person = await _personService.GetOrCreateCurrentPersonAsync( userId );
            return ( await _context.EventPersons.Where( ep => ep.PersonId == person.Id ).Select( ep => ep.Event ).Distinct( ).ToListAsync( )); 
        }

        public async Task<Event?> GetEventDetailsAsync( int eventId, string userId )
        {
            var person = await _personService.GetOrCreateCurrentPersonAsync( userId );

            return ( await _context.EventPersons.Where( ep => ep.EventId == eventId && ep.PersonId == person.Id ).Select( ep => ep.Event ).FirstOrDefaultAsync( ));
        }
    }
}
