using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Controllers.Requests.Query;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using CarAuctionManagementSystem.Handlers.Query.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers.Command
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleCommandController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult AddVehicle(
            [FromServices] IVehicleCommand handler,
            [FromQuery] AddVehicleCommand command
        )
        {
            var result = handler.Handle(command);
            return Ok(result);
        }
    }
}
