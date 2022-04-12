using Application.Participants;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ParticipantsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPut("persons/{id}")]
        public async Task<IActionResult> EditParticipant(int id, Person participant)
        {
            participant.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Participant = participant }));
        }

        [HttpPut("companies/{id}")]
        public async Task<IActionResult> EditCompanies(int id, Company participant)
        {
            participant.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Participant = participant }));
        }
    }
}
