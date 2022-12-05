using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MysteryAuction.Infrastructure.Data.Models;

namespace MysteryAuction.Infrastructure.Data
{
    public class MysteryAuctionDbContext : IdentityDbContext
    {
        public MysteryAuctionDbContext(DbContextOptions<MysteryAuctionDbContext> options)
            : base(options)
        {
        }

        public DbSet<MysteryAuctionUser> MysteryAuctionUsers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<ProductCategory> ProductsCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bid>(e =>
                e
                    .HasKey(k => new
                    {
                        k.UserId,
                        k.ProductId
                    }));


            builder.Entity<Product>(e =>
                e
                    .HasOne(mp => mp.Seller)
                    .WithMany(mau => mau.MysteryProductsForSale)
                    .HasForeignKey(mp => mp.SellerId)
                    .OnDelete(DeleteBehavior.NoAction));

            builder.Entity<Product>(e =>
                e
                    .HasOne(mp => mp.Buyer)
                    .WithMany(mau => mau.BoughtMysteryProducts)
                    .HasForeignKey(mp => mp.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction));



            base.OnModelCreating(builder);
        }
    }
}