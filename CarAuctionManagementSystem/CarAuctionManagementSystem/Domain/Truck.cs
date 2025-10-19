namespace CarAuctionManagementSystem.Domain
{
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; set; }

        public Truck(Guid id, VehicleModel model, int year, decimal startingBid, double loadCapacity)
            : base(id, model, year, startingBid)
        {
            LoadCapacity = loadCapacity;
        }
    }
}
