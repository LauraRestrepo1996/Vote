

namespace Vote.Web.Data.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Vote.Web.Helpers;

    [Route("api/[Controller]")]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class EventsController : Controller
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IEventRepository eventRepository;
        private readonly IUserHelper userHelper;
        private readonly DataContext context;

        public EventsController(IEventRepository eventRepository,
            ICandidateRepository candidateRepository,
            IUserHelper userHelper)
        {
            this.eventRepository = eventRepository;
            this.candidateRepository = candidateRepository;
            this.userHelper = userHelper;
        }

      

        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventRepository.GetAllWithUsers());
        }




      
    }
}
