using System.Linq;
using CarAuctionManagementSystem.Domain;

namespace CarAuctionManagementSystem.Persistence
{
    public static class SeedData
    {
        public static void EnsureSeedData(ApplicationDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (context.Manufacturers.Any() || context.Models.Any() || context.Vehicles.Any() || context.Auctions.Any())
                return;

            var toyota = new VehicleManufacturer("Toyota");
            var mazda = new VehicleManufacturer("Mazda");

            var corolla = toyota.AddModel("AE86");
            var hilux = toyota.AddModel("Supra");
            var rx7fc = mazda.AddModel("RX7-FC");
            var rx7fd = mazda.AddModel("RX7-FD");

            var v1 = new Sedan("AA12BBB", corolla, 2022, 12000m, 4);
            var v2 = new Truck("AA34CCC", hilux, 2021, 20000m, 1.2);
            var v3 = new Hatchback("BB56DDD", rx7fc, 2023, 10000m, 3);
            var v4 = new SUV("CC78EEE", rx7fd, 2020, 22000m, 5);

            var a1 = new Auction("corolla auction", v1, true);
            var a2 = new Auction("Hilux auction", v2, false);

            a1.AddBid(new Bid("Diogo", 13000m, a1));
            a1.AddBid(new Bid("Antonio", 14000m, a1));

            context.Manufacturers.AddRange(toyota, mazda);
            context.Vehicles.AddRange(v1, v2, v3, v4);
            context.Auctions.AddRange(a1, a2);

            context.SaveChanges();
        }
    }
}