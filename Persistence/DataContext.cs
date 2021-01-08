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
        public DbSet<UserAssets> UserAssets { get; set; }
        public DbSet<UserStaff> UserStaffs { get; set; }
        public DbSet<UserLicense> UserLicenses { get; set; }
        public DbSet<AssetsLicense> AssetsLicenses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAssets>(x => x.HasKey(ua => new { ua.AssetsId, ua.UserStaffId }));

            builder.Entity<UserAssets>()
            .HasOne(a => a.Asset)
            .WithMany(u => u.UserAssets)
            .HasForeignKey(a => a.AssetsId);

            builder.Entity<UserAssets>()
           .HasOne(u => u.UserStaff)
           .WithMany(a => a.UserAssets)
           .HasForeignKey(u => u.UserStaffId);
        }
    }
}