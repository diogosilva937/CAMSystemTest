namespace CarAuctionManagementSystem.Domain
{
    public class VehicleManufacturer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<VehicleModel> Models { get; private set; } = new();

        protected VehicleManufacturer() { } // Required by EF Core

        public VehicleManufacturer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Manufacturer name cannot be empty.", nameof(name));

            Id = Guid.NewGuid();
            Name = name.Trim();
        }
        public VehicleModel AddModel(string modelName)
        {
            if (Models.Any(m => m.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Model '{modelName}' already exists for {Name}.");

            var model = new VehicleModel(modelName, this);
            Models.Add(model);
            return model;
        }

        public override string ToString() => Name;

        public override bool Equals(object obj)
            => obj is VehicleManufacturer other && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode()
            => Name.ToLowerInvariant().GetHashCode();
    }
}
