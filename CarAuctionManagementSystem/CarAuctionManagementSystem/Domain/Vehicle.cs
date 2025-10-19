namespace CarAuctionManagementSystem.Domain
{
    public abstract class Vehicle
    {
        public Guid Id { get; private set; }
        public VehicleModel Model { get; private set; }
        public int Year { get; private set; }
        public decimal StartingBid { get; private set; }

        protected Vehicle(Guid id, VehicleModel model, int year, decimal startingBid)
        {
            Id = id;
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Year = year;
            StartingBid = startingBid;
        }

        public VehicleManufacturer Manufacturer => Model.Manufacturer;
    }
}
