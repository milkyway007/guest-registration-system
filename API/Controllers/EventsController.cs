using Application.Events.Commands;
using Application.Events.Dtos;
using Application.Events.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EventsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetEvents(string predicate)
        {
            return HandleResult(await Mediator.Send(new List.Query { Predicate = predicate }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            return HandleResult(await Mediator.Send(
                new Details.Query
                {
                    Id = id
                }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDto e)
        {
            return HandleResult(await Mediator.Send(
                new Create.Command
                {
                    Event = e
                }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(int id, Event e)
        {
            e.Id = id;
            return HandleResult(await Mediator.Send(
                new Edit.Command
                {
                    Event = e
                }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            return HandleResult(await Mediator.Send(
                new Delete.Command {
                    Id = id
                }));
        }

        [HttpGet("{id}/persons")]
        public async Task<IActionResult> GetPersons(int id)
        {
            return HandleResult(await Mediator.Send(
                new ListPersons.Query
                {
                    EventId = id
                }));
        }

        [HttpGet("{id}/companies")]
        public async Task<IActionResult> GetCompanies(int id)
        {
            return HandleResult(await Mediator.Send(
                new ListCompanies.Query
                {
                    EventId = id
                }));
        }

        [HttpPost("{eventId}/persons")]
        public async Task<IActionResult> CreatePerson(int eventId, Person person)
        {
            return HandleResult(await Mediator.Send(
                new CreateParticipation.Command
                {
                    EventId = eventId,
                    Participant = person,
                }));
        }

        [HttpPost("{eventId}/companies")]
        public async Task<IActionResult> CreateCompany(int eventId, Company company)
        {
            return HandleResult(await Mediator.Send(
                new CreateParticipation.Command
                {
                    EventId = eventId,
                    Participant = company,
                }));
        }

        [HttpDelete("{eventId}/participants/{participantCode}")]
        public async Task<IActionResult> CancelParticipation(int eventId, string participantCode)
        {
            return HandleResult(await Mediator.Send(
                new CancelParticipation.Command
                {
                    EventId = eventId,
                    ParticipantCode = participantCode
                }));
        }
    }
}
