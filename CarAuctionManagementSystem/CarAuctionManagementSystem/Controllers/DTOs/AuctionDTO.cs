namespace CarAuctionManagementSystem.Controllers.DTOs
{
    public record AuctionDTO(string auctionIdentifier, VehicleDTO? vehicle, bool? isActive);
    public record BidDTO(string auctionIdentifier, string bidderName, decimal amount);
}
