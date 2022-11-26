using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MysteryAuction.Data.Models;

namespace MysteryAuction.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MysteryAuctionUser> MysteryAuctionUsers { get; set; }

        public DbSet<MysteryProduct> MysteryProducts { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<UnclaimedContainer> UnclaimedContainers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Car>(e =>
                e
                    .HasOne(c => c.Seller)
                    .WithMany(mau => mau.CarsForSale)
                    .HasForeignKey(c => c.SellerId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<Car>(e =>
                e
                    .HasOne(c => c.Buyer)
                    .WithMany(mau => mau.BoughtCars)
                    .HasForeignKey(c => c.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<MysteryProduct>(e =>
                e
                    .HasOne(mp => mp.Seller)
                    .WithMany(mau => mau.MysteryProductsForSale)
                    .HasForeignKey(mp => mp.SellerId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<MysteryProduct>(e =>
                e
                    .HasOne(mp => mp.Buyer)
                    .WithMany(mau => mau.BoughtMysteryProducts)
                    .HasForeignKey(mp => mp.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<UnclaimedContainer>(e =>
                e
                    .HasOne(uc => uc.Seller)
                    .WithMany(mau => mau.UnclaimedContainersForSale)
                    .HasForeignKey(uc => uc.SellerId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<UnclaimedContainer>(e =>
                e
                    .HasOne(uc => uc.Buyer)
                    .WithMany(mau => mau.BoughtUnclaimedContainers)
                    .HasForeignKey(uc => uc.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction));


            base.OnModelCreating(builder);
        }
    }
}