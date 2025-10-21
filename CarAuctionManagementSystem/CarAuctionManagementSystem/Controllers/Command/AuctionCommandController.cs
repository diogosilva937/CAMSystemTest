using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers.Command
{
    [ApiController]
    [Route("auction")]
    public class AuctionCommandController: ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult AddAuction(
            [FromServices] IAuctionCommand handler,
            [FromQuery] AddAuctionCommand command
        )
        {
            var result = handler.Handle(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("bid")]
        public IActionResult AddBid(
            [FromServices] IBidCommand handler,
            [FromQuery] AddBidCommand command
        )
        {
            var result = handler.Handle(command);
            return Ok(result);
        }
    }
}
