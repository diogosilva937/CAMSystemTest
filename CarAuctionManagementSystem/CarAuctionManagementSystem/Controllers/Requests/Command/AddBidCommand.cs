namespace CarAuctionManagementSystem.Controllers.Requests.Command
{
    public class AddBidCommand
    {
        public string AuctionName { get; set; }
        public string BidderName { get; set; }
        public decimal Amount { get; set; }
    }
}
