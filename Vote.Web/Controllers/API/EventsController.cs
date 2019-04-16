﻿

namespace Vote.Web.Data.API
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]

    public class EventsController : Controller
    {
        private readonly IEventRepository eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return this.Ok(this.eventRepository.GetAllWithUsers());
        }
    }
}
