namespace CarAuctionManagementSystem.Domain
{
    public class VehicleModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid ManufacturerId { get; private set; }
        public VehicleManufacturer Manufacturer { get; private set; }

        protected VehicleModel() { } 

        public VehicleModel(string name, VehicleManufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Model name cannot be empty.", nameof(name));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));

            Id = Guid.NewGuid();
            Name = name.Trim();
            ManufacturerId = manufacturer.Id;
        }

        public override string ToString() => $"{Manufacturer?.Name} {Name}";
    }
}
