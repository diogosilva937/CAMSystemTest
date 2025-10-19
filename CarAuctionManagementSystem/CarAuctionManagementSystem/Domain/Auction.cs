namespace CarAuctionManagementSystem.Domain
{
    public class Auction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Vehicle Vehicle { get; private set; }
        public bool IsActive { get; private set; }
        public List<Bid> Bids { get; private set; } = new List<Bid>();

        public Auction(Vehicle vehicle)
        {
            Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
            IsActive = false;
        }

        public void Start()
        {
            if (IsActive)
                throw new InvalidOperationException("Auction is already active.");
            IsActive = true;
        }

        public void Close()
        {
            if (!IsActive)
                throw new InvalidOperationException("Auction is not active.");
            IsActive = false;
        }

        public void PlaceBid(Bid bid)
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot place bid on inactive auction.");

            if (Bids.Count > 0 && bid.Amount <= Bids.Max(b => b.Amount))
                throw new InvalidOperationException("Bid amount must be higher than the current highest bid.");

            Bids.Add(bid);
        }

        public Bid GetHighestBid() => Bids.OrderByDescending(b => b.Amount).FirstOrDefault();
    }
}
