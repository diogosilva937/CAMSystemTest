using CarAuctionManagementSystem.Controllers.Requests.Query;
using CarAuctionManagementSystem.Handlers.Query.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers.Query
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleQueryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetByParameters(
            [FromServices] IVehicleQuery handler,
            [FromQuery] GetVehicleQuery command
        )
        {
            var result = handler.Handle(command);
            return Ok(result);
        }
    }
}
