namespace CarAuctionManagementSystem.Domain
{
    public abstract class Vehicle
    {
        public string RegistrationNumber { get; private set; } // Primary key

        public Guid ModelId { get; private set; }
        public VehicleModel Model { get; private set; }

        public int Year { get; private set; }
        public decimal StartingBid { get; private set; }

        protected Vehicle() { } // EF Core

        protected Vehicle(string registrationNumber, VehicleModel model, int year, decimal startingBid)
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new ArgumentException("Registration number cannot be empty.", nameof(registrationNumber));

            RegistrationNumber = registrationNumber.Trim().ToUpperInvariant();
            Model = model ?? throw new ArgumentNullException(nameof(model));
            ModelId = model.Id;
            Year = year;
            StartingBid = startingBid;
        }

        public VehicleManufacturer Manufacturer => Model.Manufacturer;

        public override string ToString() => $"{RegistrationNumber} ({Model})";
    }
}
