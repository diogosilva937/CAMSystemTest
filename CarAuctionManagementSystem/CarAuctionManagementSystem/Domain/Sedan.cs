namespace CarAuctionManagementSystem.Domain
{
    public class Sedan : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Sedan(Guid id, VehicleModel model, int year, decimal startingBid, int numberOfDoors)
            : base(id, model, year, startingBid)
        {
            NumberOfDoors = numberOfDoors;
        }
    }
}
