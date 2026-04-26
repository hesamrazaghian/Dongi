using Dongi.Models;
using System.Threading.Tasks;

namespace Dongi.Services
{
    public interface IEventService
    {
        Task<int> CreateEventAsync( string title, string? description, string userId );
        Task<List<Event>> GetMyEventsAsync( string userId );
        Task<Event?> GetEventDetailsAsync( int eventId, string userId );

    }
}
