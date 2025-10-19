namespace CarAuctionManagementSystem.Domain
{
    public class VehicleModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public VehicleManufacturer Manufacturer { get; private set; }

        public VehicleModel(string name, VehicleManufacturer manufacturer)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Model name cannot be empty.", nameof(name));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));

            Id = Guid.NewGuid();
            Name = name.Trim();
        }

        public override string ToString() => $"{Manufacturer.Name} {Name}";

        public override bool Equals(object obj)
            => obj is VehicleModel other &&
               string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) &&
               Manufacturer.Equals(other.Manufacturer);

        public override int GetHashCode()
            => HashCode.Combine(Name.ToLowerInvariant(), Manufacturer);
    }
}
