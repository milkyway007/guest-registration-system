using Application.Participants.Commands;
using Application.Participants.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Participants.Events;

namespace API.Controllers
{
    public class ParticipantsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipant(int id)
        {
            return HandleResult(await Mediator.Send(
                new Details.Query
                {
                    Id = id
                }));
        }

        [HttpPut("persons/{id}")]
        public async Task<IActionResult> EditParticipant(int id, Person participant)
        {
            participant.Id = id;
            return HandleResult(await Mediator.Send(
                new Edit.Command
                {
                    Participant = participant
                }));
        }

        [HttpPut("companies/{id}")]
        public async Task<IActionResult> EditCompanies(int id, Company participant)
        {
            participant.Id = id;
            return HandleResult(await Mediator.Send(
                new Edit.Command
        {
                    Participant = participant
                }));
        }
    }
}
