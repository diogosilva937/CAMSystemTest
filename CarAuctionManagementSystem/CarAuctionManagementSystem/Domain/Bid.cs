namespace CarAuctionManagementSystem.Domain
{
    public class Bid
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BidderName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public Bid(string bidderName, decimal amount)
        {
            BidderName = bidderName;
            Amount = amount;
        }
    }
}
