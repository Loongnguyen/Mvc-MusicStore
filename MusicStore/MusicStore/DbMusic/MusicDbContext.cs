using MusicStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MusicStore.Data
{
    public class MusicDbContext : IdentityDbContext<AppUser>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options)
          : base(options)
        {
        }
        public DbSet<Album> Album { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           

            base.OnModelCreating(builder);

            builder.Entity<OrderDetail>()
                .HasOne(d => d.Order)
                .WithMany(d => d.OrderDetails)
                .HasForeignKey(d => d.OrderId);

            builder.Entity<OrderDetail>()
                 .HasOne(d => d.Album)
                 .WithMany(d => d.OrderDetails)
                 .HasForeignKey(d => d.AlbumId);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
