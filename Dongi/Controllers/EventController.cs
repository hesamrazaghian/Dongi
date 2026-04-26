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

        [HttpGet]
        public async Task<IActionResult> Index( )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if( userId == null )
                return Challenge( );

            var events = await _eventService.GetMyEventsAsync(userId);

            var model = events.Select(e => new MyEventListItemViewModel
            {
                Id = e.Id,
                Title = e.Title
            }).ToList();

            return View( model );
        }

        [HttpGet]
        public async Task<IActionResult> Details( int id )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if( userId == null )
                return Challenge( );

            var ev = await _eventService.GetEventDetailsAsync(id, userId);
            if( ev == null )
                return NotFound( );

            var model = new EventDetailsViewModel
            {
                Id = ev.Id,
                Title = ev.Title
            };

            return View( model );
        }

    }
}
