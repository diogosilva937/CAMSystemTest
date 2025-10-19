namespace CarAuctionManagementSystem.Domain
{
    public class VehicleManufacturer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public VehicleManufacturer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Manufacturer name cannot be empty.", nameof(name));

            Id = Guid.NewGuid();
            Name = name.Trim();
        }

        public override string ToString() => Name;

        public override bool Equals(object obj)
            => obj is VehicleManufacturer other && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode()
            => Name.ToLowerInvariant().GetHashCode();
    }
}
