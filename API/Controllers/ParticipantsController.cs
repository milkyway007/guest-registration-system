using Application.Participants.Commands;
using Application.Participants.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ParticipantsController : BaseApiController
    {
        [HttpGet("{code}")]
        public async Task<ActionResult<Participant>> GetParticipant(string code)
        {
            return HandleResult(await Mediator.Send(
                new Details.Query
                {
                    Code = code
                }));
        }

        [HttpPut("persons/{code}")]
        public async Task<IActionResult> EditParticipant(string code, Person participant)
        {
            participant.Code = code;
            return HandleResult(await Mediator.Send(
                new Edit.Command
                {
                    Participant = participant
                }));
        }

        [HttpPut("companies/{code}")]
        public async Task<IActionResult> EditCompanies(string code, Company participant)
        {
            participant.Code = code;
            return HandleResult(await Mediator.Send(
                new Edit.Command
        {
                    Participant = participant
                }));
        }
    }
}
