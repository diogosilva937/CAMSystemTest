using System.Linq;
using CarAuctionManagementSystem.Domain;

namespace CarAuctionManagementSystem.Persistence
{
    public static class SeedData
    {
        public static void EnsureSeedData(ApplicationDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            // evita popular mais de uma vez
            if (context.Manufacturers.Any() || context.Models.Any() || context.Vehicles.Any() || context.Auctions.Any())
                return;

            // Criar fabricantes e modelos via domínio (mantém invariantes e FK consistente)
            var toyota = new VehicleManufacturer("Toyota");
            var mazda = new VehicleManufacturer("Mazda");

            var corolla = toyota.AddModel("AE86");
            var hilux = toyota.AddModel("Supra");
            var fiesta = mazda.AddModel("Fiesta");
            var ranger = mazda.AddModel("Ranger");

            // Criar veículos vinculando aos modelos
            var v1 = new Sedan("AA12BBB", corolla, 2022, 12000m, 4);
            var v2 = new Truck("AA34CCC", hilux, 2021, 20000m, 1.2);
            var v3 = new Hatchback("BB56DDD", fiesta, 2023, 10000m, 3);
            var v4 = new SUV("CC78EEE", ranger, 2020, 22000m, 5);

            // Leilões e lances
            var a1 = new Auction("corolla auction", v1, true);
            var a2 = new Auction("Hilux auction", v2, false);

            // Adicionando alguns lances de exemplo (a1 está ativo)
            a1.AddBid(new Bid("Diogo", 13000m, a1));
            a1.AddBid(new Bid("Antonio", 14000m, a1));

            // Persistir: adiciona fabricantes (com modelos) e veículos/leilões
            context.Manufacturers.AddRange(toyota, mazda);
            context.Vehicles.AddRange(v1, v2, v3, v4);
            context.Auctions.AddRange(a1, a2);

            context.SaveChanges();
        }
    }
}