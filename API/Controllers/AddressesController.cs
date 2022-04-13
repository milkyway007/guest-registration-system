using Application.Addresses.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AddressesController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAddress(Address address)
        {
            return HandleResult(await Mediator.Send(
                new Create.Command
                {
                    Address = address
                }));
        }
    }
}
