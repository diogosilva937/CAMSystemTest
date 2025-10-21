using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Command;

namespace CarAuctionManagementSystem.Handlers.Command.Interfaces
{
    public interface IAuctionCommand
    {
        AuctionDTO Handle(AddAuctionCommand auctionCommand);

    }
}
