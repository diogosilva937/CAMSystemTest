namespace CarAuctionManagementSystem.Domain
{
    public class Hatchback : Vehicle
    {
        public int NumberOfDoors { get; set; }

        protected Hatchback() { }

        public Hatchback(string registrationNumber, VehicleModel model, int year, decimal startingBid, int numberOfDoors)
            : base(registrationNumber, model, year, startingBid)
        {
            NumberOfDoors = numberOfDoors;
        }
    }
}
