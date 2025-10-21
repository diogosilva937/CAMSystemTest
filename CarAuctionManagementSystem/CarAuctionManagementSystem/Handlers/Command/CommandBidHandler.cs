using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Domain;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using CarAuctionManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionManagementSystem.Handlers.Command
{
    public class CommandBidHandler : IBidCommand
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandBidHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BidDTO Handle(AddBidCommand bidCommand)
        {
            if(!_dbContext.Auctions.Where(a => a.AuctionName.Equals(bidCommand.AuctionName) && a.IsActive).Any())
            {
                throw new InvalidOperationException($"Auction with name '{bidCommand.AuctionName}' does not exist or is not active.");
            }
            
            var auction = _dbContext.Auctions
                                    .Where(a => a.AuctionName.Equals(bidCommand.AuctionName))
                                    .Include(a => a.Bids)
                                    .FirstOrDefault();

            if(auction.Bids.Count > 0 && auction.Bids.MaxBy(b => b.Amount).Amount > bidCommand.Amount)
            {
                throw new InvalidOperationException($"Bid amount is not higher than the current highest bid ({auction.Bids.MaxBy(b => b.Amount).Amount})");
            }

            _dbContext.Bids.Add(new Bid(bidCommand.BidderName, bidCommand.Amount, auction));
            _dbContext.SaveChanges();

            return new BidDTO
            (
                auction.AuctionName,
                bidCommand.BidderName,
                bidCommand.Amount
            );
        }
    }
}
