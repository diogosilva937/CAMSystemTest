using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Domain;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using CarAuctionManagementSystem.Persistence;

namespace CarAuctionManagementSystem.Handlers.Command
{
    public class CommandAuctionCloseHandler : IAuctionCloseCommand
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandAuctionCloseHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AuctionDTO HandleClose(AddAuctionCommand auctionCommand)
        {
            Auction auction = _dbContext.Auctions
                .FirstOrDefault(a => a.AuctionName == auctionCommand.AuctionName);
            
            if(auction is null)
            {
                throw new InvalidOperationException($"Auction with name {auctionCommand.AuctionName} does not exist.");
            }

            if(!auction.IsActive)
            {
                throw new InvalidOperationException($"Auction {auctionCommand.AuctionName} is already closed.");
            }

            auction.SetActive(false);
            _dbContext.SaveChanges();
            return new AuctionDTO
            (
                auction.AuctionName,
                new VehicleDTO
                (
                    auction.Vehicle.RegistrationNumber,
                    new VehicleModelDTO
                    (
                        auction.Vehicle.Model.Name,
                        new VehicleManufacturerDTO(auction.Vehicle.Model.Manufacturer.Name)
                    ),
                    auction.Vehicle.Year,
                    auction.Vehicle.StartingBid,
                    auction.Vehicle.GetType().Name
                ),
                auction.IsActive
            );
        }
    }
}
