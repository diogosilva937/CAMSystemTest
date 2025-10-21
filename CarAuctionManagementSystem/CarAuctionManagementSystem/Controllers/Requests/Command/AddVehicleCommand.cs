using CarAuctionManagementSystem.Domain;

namespace CarAuctionManagementSystem.Controllers.Requests.Command
{
    public class AddVehicleCommand
    {
        public string RegistrationNumber { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string ModelName { get; set; }
        public string ManufacturerName { get; set; }
        public decimal? StartingBid { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? NumberOfDoors { get; set; }
        public double? LoadCapacity { get; set; }
    }
}
