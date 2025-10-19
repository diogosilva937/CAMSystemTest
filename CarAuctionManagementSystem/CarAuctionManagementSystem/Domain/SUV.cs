namespace CarAuctionManagementSystem.Domain
{
    public class SUV : Vehicle
    {
        public int NumberOfSeats { get; set; }

        public SUV(Guid id, VehicleModel model, int year, decimal startingBid, int numberOfSeats)
            : base(id, model, year, startingBid)
        {
            NumberOfSeats = numberOfSeats;
        }
    }
}
