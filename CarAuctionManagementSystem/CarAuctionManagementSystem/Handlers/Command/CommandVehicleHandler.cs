using CarAuctionManagementSystem.Controllers.DTOs;
using CarAuctionManagementSystem.Controllers.Requests.Command;
using CarAuctionManagementSystem.Domain;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using CarAuctionManagementSystem.Persistence;

namespace CarAuctionManagementSystem.Handlers.Command
{
    public class CommandVehicleHandler : IVehicleCommand
    {
        private readonly ApplicationDbContext _dbContext;

        public CommandVehicleHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public VehicleDTO Handle(AddVehicleCommand vehicleCommand)
        {
            switch (vehicleCommand.Type) {
                case "SUV":
                    if(vehicleCommand.NumberOfDoors is not null)
                    {
                        _dbContext.Vehicles.Add(new SUV(
                            vehicleCommand.RegistrationNumber,
                            new VehicleModel(vehicleCommand.ModelName, new VehicleManufacturer(vehicleCommand.ManufacturerName)),
                            vehicleCommand.Year,
                            vehicleCommand.StartingBid ?? 0,
                            vehicleCommand.NumberOfDoors.Value
                        ));
                    }
                    break;
                case "Sedan":
                    if(vehicleCommand.NumberOfSeats is not null)
                    {
                        _dbContext.Vehicles.Add(new Sedan(
                            vehicleCommand.RegistrationNumber,
                            new VehicleModel(vehicleCommand.ModelName, new VehicleManufacturer(vehicleCommand.ManufacturerName)),
                            vehicleCommand.Year,
                            vehicleCommand.StartingBid ?? 0,
                            vehicleCommand.NumberOfSeats.Value
                        ));
                    }
                    break;
                case "Hatchback":
                    if(vehicleCommand.NumberOfDoors is not null)
                    {
                        _dbContext.Vehicles.Add(new Hatchback(
                            vehicleCommand.RegistrationNumber,
                            new VehicleModel(vehicleCommand.ModelName, new VehicleManufacturer(vehicleCommand.ManufacturerName)),
                            vehicleCommand.Year,
                            vehicleCommand.StartingBid ?? 0,
                            vehicleCommand.NumberOfDoors.Value
                        ));
                    }
                    break;
                case "Truck":
                    if(vehicleCommand.LoadCapacity is not null)
                    {
                        _dbContext.Vehicles.Add(new Truck(
                            vehicleCommand.RegistrationNumber,
                            new VehicleModel(vehicleCommand.ModelName, new VehicleManufacturer(vehicleCommand.ManufacturerName)),
                            vehicleCommand.Year,
                            vehicleCommand.StartingBid ?? 0,
                            vehicleCommand.LoadCapacity.Value
                        ));
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            _dbContext.SaveChanges();

            return new VehicleDTO
            (
                vehicleCommand.RegistrationNumber,
                new VehicleModelDTO(vehicleCommand.ModelName, new VehicleManufacturerDTO(vehicleCommand.ManufacturerName)),
                vehicleCommand.Year,
                vehicleCommand.StartingBid ?? 0,
                vehicleCommand.Type
            );
        }
    }
}
