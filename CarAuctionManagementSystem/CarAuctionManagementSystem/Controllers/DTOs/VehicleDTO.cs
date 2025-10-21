using CarAuctionManagementSystem.Domain;

namespace CarAuctionManagementSystem.Controllers.DTOs
{
    public record SUVDto(string registrationNumber, VehicleModelDTO model, int year, decimal startingBid, int numberOfSeats);

    public record TruckDTO(string registrationNumber, VehicleModelDTO model, int year, decimal startingBid, double loadCapacity);

    public record HatchbackDTO(string registrationNumber, VehicleModelDTO model, int year, decimal startingBid, int numberOfDoors);

    public record SedanDTO(string registrationNumber, VehicleModelDTO model, int year, decimal startingBid, int numberOfDoors);

    public record VehicleDTO(string registrationNumber, VehicleModelDTO model, int year, decimal startingBid, string? type);

    public record VehicleModelDTO(string name, VehicleManufacturerDTO manufacturer);

    public record VehicleManufacturerDTO(string name);


}
