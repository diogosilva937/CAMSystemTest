namespace CarAuctionManagementSystem.Domain
{
    public class Auction
    {
        public string AuctionName { get; set; }
        public string VehicleRegistrationNumber { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public bool IsActive { get; private set; }
        public List<Bid> Bids { get; private set; } = new List<Bid>();

        protected Auction() { }

        public Auction(string AuctionName, Vehicle vehicle, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(AuctionName))
                throw new ArgumentException("Auction name cannot be empty.", nameof(AuctionName));
            this.AuctionName = AuctionName;
            Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
            VehicleRegistrationNumber = vehicle.RegistrationNumber;
            IsActive = isActive;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void AddBid(Bid bid)
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot place bid on inactive auction.");
            Bids.Add(bid);
        }

        public Bid GetHighestBid() => Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
    }
}
