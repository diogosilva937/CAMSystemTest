namespace CarAuctionManagementSystem.Domain
{
    public class SUV : Vehicle
    {
        public int NumberOfSeats { get; set; }

        protected SUV() { }

        public SUV(string registrationNumber, VehicleModel model, int year, decimal startingBid, int numberOfSeats)
            : base(registrationNumber, model, year, startingBid)
        {
            NumberOfSeats = numberOfSeats;
        }
    }
}
