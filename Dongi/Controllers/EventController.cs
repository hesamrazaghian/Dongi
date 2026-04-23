using Dongi.Services;
using Dongi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dongi.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController( IEventService eventService )
        {
            _eventService = eventService;
        }

        [HttpGet]
        public IActionResult Create( )
        {
            return View( );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateEventViewModel model )
        {
            if( !ModelState.IsValid )
                return View( model );

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _eventService.CreateEventAsync(
                model.Title,
                model.Description,
                userId! );

            return RedirectToAction( "Index", "Home" );
        }
    }
}
