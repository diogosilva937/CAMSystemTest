namespace CarAuctionManagementSystem.Domain
{
    public class Hatchback : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Hatchback(Guid id, VehicleModel model, int year, decimal startingBid, int numberOfDoors)
            : base(id, model, year, startingBid)
        {
            NumberOfDoors = numberOfDoors;
        }
    }
}
