namespace CarAuctionManagementSystem.Controllers.Requests.Command
{
    public class AddAuctionCommand
    {
        public string AuctionName { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public bool isActive { get; set; }
        }
}
