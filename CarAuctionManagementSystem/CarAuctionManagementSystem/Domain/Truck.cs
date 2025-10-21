namespace CarAuctionManagementSystem.Domain
{
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; set; }

        protected Truck() { }

        public Truck(string registrationNumber, VehicleModel model, int year, decimal startingBid, double loadCapacity)
            : base(registrationNumber, model, year, startingBid)
        {
            LoadCapacity = loadCapacity;
        }
    }
}
