using Dongi.Models;

public interface IPersonService
{
    Task<Person> GetOrCreateCurrentPersonAsync( string userId );
}
