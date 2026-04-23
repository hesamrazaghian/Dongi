using System.Threading.Tasks;

namespace Dongi.Services
{
    public interface IEventService
    {
        Task<int> CreateEventAsync( string title, string? description, string userId );
    }
}
