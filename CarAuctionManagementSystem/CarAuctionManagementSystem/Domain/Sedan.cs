namespace CarAuctionManagementSystem.Domain
{
    public class Sedan : Vehicle
    {
        public int NumberOfDoors { get; private set; }

        protected Sedan() { } // EF Core

        public Sedan(string registrationNumber, VehicleModel model, int year, decimal startingBid, int numberOfDoors)
            : base(registrationNumber, model, year, startingBid)
        {
            if (numberOfDoors <= 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfDoors), "Number of doors must be positive.");

            NumberOfDoors = numberOfDoors;
        }
    }
}
