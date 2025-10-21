using CarAuctionManagementSystem.Domain;

namespace CarAuctionManagementSystem.Controllers.Requests.Query
{
    public class GetVehicleQuery
    {
        public string? Type { get;  set; } 

        public string? Manufacturer { get;  set; }

        public int? Year { get;  set; }
        public string? Model { get;  set; }
    }
}
