using CarAuctionManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionManagementSystem.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleManufacturer> Manufacturers { get; set; }
        public DbSet<VehicleModel> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- VehicleManufacturer
            modelBuilder.Entity<VehicleManufacturer>(entity =>
            {
                entity.ToTable("Manufacturers");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(m => m.Name).IsUnique();

            });

            // --- VehicleModel
            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.ToTable("Models");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(m => new { m.ManufacturerId, m.Name }).IsUnique();

                entity.HasOne(m => m.Manufacturer)
                      .WithMany(man => man.Models)
                      .HasForeignKey(m => m.ManufacturerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // --- Vehicle
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicles");
                entity.HasKey(v => v.RegistrationNumber);

                entity.Property(v => v.RegistrationNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.HasOne(v => v.Model)
                      .WithMany()
                      .HasForeignKey(v => v.ModelId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(v => v.Year).IsRequired();
                entity.Property(v => v.StartingBid).HasColumnType("decimal(18,2)");

                entity.HasDiscriminator<string>("VehicleType")
                      .HasValue<Hatchback>("Hatchback")
                      .HasValue<Sedan>("Sedan")
                      .HasValue<SUV>("SUV")
                      .HasValue<Truck>("Truck");
            });

            modelBuilder.Entity<Bid>()
             .Property(b => b.Id)
             .ValueGeneratedOnAdd()
             .HasDefaultValueSql("NEWSEQUENTIALID()");

            // --- Auction + Bids 
            modelBuilder.Entity<Auction>(entity =>
            {
                entity.ToTable("Auctions");
                entity.HasKey(a => a.AuctionName);
                entity.Property(v => v.AuctionName)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.HasOne(a => a.Vehicle)
                      .WithMany()
                      .HasForeignKey(a => a.VehicleRegistrationNumber)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(a => a.IsActive).IsRequired();
            });

            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("Bids");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id)
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.Property(b => b.BidderName).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(b => b.Timestamp).IsRequired();
                entity.HasOne(b => b.BidAuction)
                      .WithMany(auc => auc.Bids)
                      .HasForeignKey(b => b.AuctionName)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("carauctionsystem");
        }
    }
}