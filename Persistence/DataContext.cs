using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<AssetHistory> AssetHistories { get; set; }
        public DbSet<LicenseHistory> LicenseHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AssetHistory>(ah =>
            {
                ah.HasKey(k => new { k.AppUserId, k.AssetId });

                ah.HasOne(u => u.AppUser)
                .WithMany(a => a.AssetHistories)
                .HasForeignKey(u => u.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<LicenseHistory>(lh =>
            {
                lh.HasKey(k => new { k.AppUserId, k.LicenseId });

                lh.HasOne(u => u.AppUser)
                .WithMany(a => a.LicenseHistories)
                .HasForeignKey(u => u.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}