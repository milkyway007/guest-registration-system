using Domain;
using Microsoft.AspNetCore.Mvc;
using Participants.Events;

namespace API.Controllers
{
    public class ParticipantsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditParticipant(int id, Participant e)
        {
            e.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Participant = e }));
        }
    }
}
