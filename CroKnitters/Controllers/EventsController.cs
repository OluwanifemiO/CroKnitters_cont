using CroKnitters.Entities;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace CroKnitters.Controllers
{
    public class EventsController : Controller
    {

        public EventsController(CrochetAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var events = _dbContext.Events.Include(e => e.EventUsers).ToList();

            EventViewModel viewModel = new EventViewModel()
            {
                Events = events
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View("CreateEvent");
        }

        //accept the request containing all the relevant details
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDetailsViewModel EventViewModel)
        {
            //get the current user id
            int userId = Int32.Parse(Request.Cookies["userId"]!);

            //check if there are no errors
            if (ModelState.IsValid)
            {
                //valid user
                if (userId != 0)
                {
                    EventViewModel.ActiveEvent.OwnerId = userId;
                    EventViewModel.ActiveEvent.Owner = _dbContext.Users.FirstOrDefault(o => o.UserId == userId);

                    EventViewModel.ActiveEvent.EventUsers.Add(new EventUser { User = EventViewModel.ActiveEvent.Owner });

                    //add the event
                    _dbContext.Events.Add(EventViewModel.ActiveEvent);
                    //save
                    await _dbContext.SaveChangesAsync();

                    TempData["LastActionMessage"] = $"New Project \"{EventViewModel.ActiveEvent.EventId}\" was added.";

                    return RedirectToAction("Index", "Events");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                // Log or inspect ModelState errors
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                Console.WriteLine("something went wrong");
                return View("CreateEvent", EventViewModel);
            }

        }

        public async Task<IActionResult> DeleteEvent(int id)
        {
            // Find the event
            var Event = await _dbContext.Events
                   .Include(p => p.EventUsers)
                   .FirstOrDefaultAsync(p => p.EventId == id);

            if (Event != null)
            {
                if (Event.EventUsers != null)
                {
                    _dbContext.EventUsers.RemoveRange(Event.EventUsers);
                }

                // Remove the event
                _dbContext.Events.Remove(Event);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Events");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            //find the event with the id
            var existingEvent = await _dbContext.Events.Include(p => p.EventUsers)
                .FirstOrDefaultAsync(p => p.EventId == id);

            //if the evtn exists
            if (existingEvent != null)
            {
                EventDetailsViewModel viewModel = new EventDetailsViewModel()
                {
                    ActiveEvent = existingEvent
                };

                return View(viewModel);
            }
            else return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EventDetailsViewModel EventViewModel)
        {

            if (ModelState.IsValid && (EventViewModel.ActiveEvent.EventId != 0))
            {
                int userId = Int32.Parse(Request.Cookies["userId"]!);

                EventViewModel.ActiveEvent.OwnerId = userId;

                _dbContext.Events.Update(EventViewModel.ActiveEvent);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Events");
            }
            else
            {
                // Log or inspect ModelState errors
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                Console.WriteLine("something went wrong");
                return View("EditEvent", EventViewModel);
            }

        }

        public async Task<IActionResult> RegisterUser(int UserId, int EventId)
        {

            var Event = await _dbContext.Events
                  .Include(p => p.EventUsers)
                  .FirstOrDefaultAsync(p => p.EventId == EventId);

            if (Event != null)
            {
                if (Event.EventUsers != null)
                {
                    var User = _dbContext.Users.FirstOrDefault(o => o.UserId == UserId);
                    Event.EventUsers.Add(new EventUser { User = User });
                }

                _dbContext.Events.Update(Event);

                //Save the changes to the Database
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Events");
            }
            else
            {
                return RedirectToAction("Index", "Events");
            }

        }


        public async Task<IActionResult> UnregisterUser(int UserId, int EventId)
        {
            var EventUser = await _dbContext.EventUsers
                  .Include(p => p.Event).Where(p => p.UserId == UserId)
                  .FirstOrDefaultAsync(p => p.EventId == EventId);

            if (EventUser != null)
            {

                Console.WriteLine($"User: {EventUser.UserId} Has been REMOVED(?)");

                _dbContext.EventUsers.Remove(EventUser);

                //Save the changes to the Database
                await _dbContext.SaveChangesAsync();

                Console.WriteLine($"DATABASE UPDATED BABY!!!");

                return RedirectToAction("Index", "Events");
            }
            else
            {
                return RedirectToAction("Index", "Events");
            }

        }

        private CrochetAppDbContext _dbContext;


    }
}
