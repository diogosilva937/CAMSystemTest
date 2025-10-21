using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Domain;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using CarAuctionManagementSystem.Mapper;
using CarAuctionManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionManagementSystem.Handlers.Command
{
    public class CommandAuctionHandler : IAuctionCommand
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandAuctionHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public AuctionDTO Handle(AddAuctionCommand auctionCommand)
        {
            IQueryable<Auction> auctions = _dbContext.Auctions
                                        .Include(a => a.Vehicle);

            if(auctions.Where(a=> a.Vehicle.RegistrationNumber.Equals(auctionCommand.VehicleRegistrationNumber) && a.IsActive).Any())
            {
                throw new InvalidOperationException("An auction for this vehicle already exists in an active auction.");
            }

            _dbContext.Add(new Auction(
                auctionCommand.AuctionName,
                _dbContext.Vehicles
                    .FirstOrDefault(v => v.RegistrationNumber.Equals(auctionCommand.VehicleRegistrationNumber))
                    ?? throw new InvalidOperationException("Vehicle not found."),
                auctionCommand.isActive
            ));

            _dbContext.SaveChanges();
            return new AuctionDTO
            (
                auctionCommand.AuctionName,
                VehicleMapper.DomainToDTO(
                        _dbContext.Vehicles
                            .Include(v => v.Model)
                            .ThenInclude(m => m.Manufacturer)
                            .FirstOrDefault(v => v.RegistrationNumber.Equals(auctionCommand.VehicleRegistrationNumber))
                            ?? throw new InvalidOperationException("Vehicle not found.")
                    )
                ,
                auctionCommand.isActive
            );
        }
    }
}
