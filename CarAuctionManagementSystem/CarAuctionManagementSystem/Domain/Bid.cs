namespace CarAuctionManagementSystem.Domain
{
    public class Bid
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BidderName { get; set; }
        public decimal Amount { get; set; }
        public Auction BidAuction { get; set; }

        public string AuctionName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        protected Bid() { } // Required by EF Core

        public Bid(string bidderName, decimal amount, Auction bidAuction)
        {
            BidderName = bidderName;
            Amount = amount;
            BidAuction = bidAuction;
            AuctionName = bidAuction.AuctionName;
        }
    }
}
