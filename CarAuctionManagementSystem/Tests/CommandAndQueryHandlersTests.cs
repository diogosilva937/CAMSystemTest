using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Controllers.Requests.Query;
using CarAuctionManagementSystem.Handlers.Command;
using CarAuctionManagementSystem.Handlers.Query;
using CarAuctionManagementSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class CommandAndQueryHandlersTests
    {
        private ApplicationDbContext CreateContext(string dbName = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AddVehicle_ShouldThrowWhenRegistrationAlreadyExists()
        {
            using var ctx = CreateContext();
            var vehicleHandler = new CommandVehicleHandler(ctx);
            var cmd = new AddVehicleCommand
            {
                RegistrationNumber = "REG-123",
                Type = "Sedan",
                Year = 2020,
                ModelName = "ModelA",
                ManufacturerName = "MakeA",
                StartingBid = 1000m,
                NumberOfSeats = 4
            };

            var created = vehicleHandler.Handle(cmd);

            var duplicate = new AddVehicleCommand
            {
                RegistrationNumber = "REG-123",
                Type = "Sedan",
                Year = 2021,
                ModelName = "ModelB",
                ManufacturerName = "MakeB",
                StartingBid = 1500m,
                NumberOfSeats = 4
            };

            Assert.Throws<InvalidOperationException>(() => vehicleHandler.Handle(duplicate));
        }

        [Fact]
        public void StartAuction_ShouldFailWhenVehicleDoesNotExist()
        {
            using var ctx = CreateContext();
            var auctionHandler = new CommandAuctionHandler(ctx);

            var cmd = new AddAuctionCommand
            {
                AuctionName = "A1",
                VehicleRegistrationNumber = "NONEXISTENT",
                isActive = true
            };

            Assert.Throws<InvalidOperationException>(() => auctionHandler.Handle(cmd));
        }

        [Fact]
        public void StartAuction_ShouldFailWhenVehicleAlreadyInActiveAuction()
        {
            using var ctx = CreateContext();
            var vehicleHandler = new CommandVehicleHandler(ctx);
            var auctionHandler = new CommandAuctionHandler(ctx);

            var vehicleCmd = new AddVehicleCommand
            {
                RegistrationNumber = "V-100",
                Type = "SUV",
                Year = 2019,
                ModelName = "M100",
                ManufacturerName = "MakerX",
                StartingBid = 500m,
                NumberOfDoors = 5
            };

            vehicleHandler.Handle(vehicleCmd);

            var auctionCmd1 = new AddAuctionCommand
            {
                AuctionName = "AUCTION-1",
                VehicleRegistrationNumber = "V-100",
                isActive = true
            };

            var createdAuction = auctionHandler.Handle(auctionCmd1);


            var auctionCmd2 = new AddAuctionCommand
            {
                AuctionName = "AUCTION-2",
                VehicleRegistrationNumber = "V-100",
                isActive = true
            };

            Assert.Throws<InvalidOperationException>(() => auctionHandler.Handle(auctionCmd2));
        }

        [Fact]
        public void PlaceBid_ShouldFailWhenAuctionNotActive()
        {

            using var ctx = CreateContext();
            var vehicleHandler = new CommandVehicleHandler(ctx);
            var auctionHandler = new CommandAuctionHandler(ctx);
            var bidHandler = new CommandBidHandler(ctx);

            var vehicleCmd = new AddVehicleCommand
            {
                RegistrationNumber = "V-BID-1",
                Type = "Hatchback",
                Year = 2018,
                ModelName = "H1",
                ManufacturerName = "MakeH",
                StartingBid = 300m,
                NumberOfDoors = 3
            };

            vehicleHandler.Handle(vehicleCmd);

            var auctionCmd = new AddAuctionCommand
            {
                AuctionName = "AUCTION-INACTIVE",
                VehicleRegistrationNumber = "V-BID-1",
                isActive = false
            };

            auctionHandler.Handle(auctionCmd);

            var bidCmd = new AddBidCommand
            {
                AuctionName = "AUCTION-INACTIVE",
                BidderName = "BidderA",
                Amount = 400m
            };


            Assert.Throws<InvalidOperationException>(() => bidHandler.Handle(bidCmd));
        }

        [Fact]
        public void PlaceBid_ShouldFailWhenAmountNotHigherThanCurrentHighest()
        {
            using var ctx = CreateContext();
            var vehicleHandler = new CommandVehicleHandler(ctx);
            var auctionHandler = new CommandAuctionHandler(ctx);
            var bidHandler = new CommandBidHandler(ctx);

            var vehicleCmd = new AddVehicleCommand
            {
                RegistrationNumber = "V-BID-2",
                Type = "Truck",
                Year = 2021,
                ModelName = "T1",
                ManufacturerName = "MakeT",
                StartingBid = 1000m,
                LoadCapacity = 2.5
            };

            vehicleHandler.Handle(vehicleCmd);

            var auctionCmd = new AddAuctionCommand
            {
                AuctionName = "AUCTION-ACTIVE",
                VehicleRegistrationNumber = "V-BID-2",
                isActive = true
            };
            auctionHandler.Handle(auctionCmd);

            var initialBid = new AddBidCommand
            {
                AuctionName = "AUCTION-ACTIVE",
                BidderName = "First",
                Amount = 1500m
            };
            bidHandler.Handle(initialBid);

            var lowBid = new AddBidCommand
            {
                AuctionName = "AUCTION-ACTIVE",
                BidderName = "Second",
                Amount = 1400m
            };

            Assert.Throws<InvalidOperationException>(() => bidHandler.Handle(lowBid));
        }

        [Fact]
        public void PlaceBid_ShouldSucceedWhenValid()
        {
            using var ctx = CreateContext();
            var vehicleHandler = new CommandVehicleHandler(ctx);
            var auctionHandler = new CommandAuctionHandler(ctx);
            var bidHandler = new CommandBidHandler(ctx);

            var vehicleCmd = new AddVehicleCommand
            {
                RegistrationNumber = "V-BID-3",
                Type = "Sedan",
                Year = 2022,
                ModelName = "S-22",
                ManufacturerName = "MakeS",
                StartingBid = 800m,
                NumberOfSeats = 5
            };

            vehicleHandler.Handle(vehicleCmd);

            var auctionCmd = new AddAuctionCommand
            {
                AuctionName = "AUCTION-OK",
                VehicleRegistrationNumber = "V-BID-3",
                isActive = true
            };
            auctionHandler.Handle(auctionCmd);

            var firstBid = new AddBidCommand
            {
                AuctionName = "AUCTION-OK",
                BidderName = "Alice",
                Amount = 900m
            };

            var result = bidHandler.Handle(firstBid);

            Assert.Equal("AUCTION-OK", result.auctionIdentifier);
            Assert.Equal("Alice", result.bidderName);
            Assert.Equal(900m, result.amount);


            var bidsInDb = ctx.Bids.Where(b => b.AuctionName == "AUCTION-OK").ToList();
            Assert.Single(bidsInDb);
            Assert.Equal(900m, bidsInDb[0].Amount);
        }

        [Fact]
        public void QueryVehicleHandler_FilteringBehaviours()
        {

            using var ctx = CreateContext();
            var vehicleHandler = new CommandVehicleHandler(ctx);
            var queryHandler = new QueryVehicleHandler(ctx);

            vehicleHandler.Handle(new AddVehicleCommand
            {
                RegistrationNumber = "Q-1",
                Type = "SUV",
                Year = 2015,
                ModelName = "M1",
                ManufacturerName = "MakeA",
                StartingBid = 500m,
                NumberOfDoors = 5
            });

            vehicleHandler.Handle(new AddVehicleCommand
            {
                RegistrationNumber = "Q-2",
                Type = "Sedan",
                Year = 2015,
                ModelName = "M1",
                ManufacturerName = "MakeA",
                StartingBid = 600m,
                NumberOfSeats = 5
            });

            var queryByType = new GetVehicleQuery { Type = "SUV" };
            var suvList = queryHandler.Handle(queryByType);
            Assert.Single(suvList);
            Assert.Equal("Q-1", suvList[0].registrationNumber);

            var queryByMakeModel = new GetVehicleQuery { Manufacturer = "MakeA", Model = "M1" };
            var listByMakeModel = queryHandler.Handle(queryByMakeModel);
            Assert.Equal(2, listByMakeModel.Count);

            var queryByYear = new GetVehicleQuery { Year = 2015 };
            var listByYear = queryHandler.Handle(queryByYear);
            Assert.Equal(2, listByYear.Count);
        }
    }
}