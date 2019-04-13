
namespace Vote.Web.Controllers
{


    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;


    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserHelper userHelper;

        public EventsController(IEventRepository eventRepository, IUserHelper userHelper) // ICandidateRepository candidateRepository
        {
            this.eventRepository = eventRepository;
        //  this.candidateRepository = candidateRepository;
            this.userHelper = userHelper;
        }

        // GET: Events
        public IActionResult Index()
        {
            return View(this.eventRepository.GetAll());
        }

       
        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                // TODO: Pending to change to: this.User.Identity.Name
                @event.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                await this.eventRepository.CreateAsync(@event);
                return RedirectToAction(nameof(Index));
            }

            return View(@event);
        }


        
            // GET: Events/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event @event)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Pending to change to: this.User.Identity.Name
                   @event.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                    await this.eventRepository.UpdateAsync(@event);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.eventRepository.ExistAsync(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await this.eventRepository.GetByIdAsync(id.Value);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await this.eventRepository.GetByIdAsync(id);
            await this.eventRepository.DeleteAsync(@event);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateCand()
        {
            return View("../Candidates/Create"); //"../Candidates/Create"
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCand(Candidate candidate)
        {

            if (ModelState.IsValid)
            {
                await this.candidateRepository.CreateAsync(candidate);
                return RedirectToAction();
                
            }
            return View(candidate);


        }
    }

}
