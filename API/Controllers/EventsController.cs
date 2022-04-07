using Application.Events;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EventsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event e)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Event = e}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(int id, Event e)
        {
            e.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Event = e }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
