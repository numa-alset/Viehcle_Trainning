using Microsoft.EntityFrameworkCore;
using VehicleApi.Core.Models;

namespace VehicleApi.Persistance
{
    public class VehicleApiDbContext : DbContext
    {
        public VehicleApiDbContext(DbContextOptions<VehicleApiDbContext>options): base(options) { }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Features)
                .WithMany(f => f.Vehicles)
                .UsingEntity<FeatureVehicle>(
                j=>j.Property(e=>e.CreateOn).HasDefaultValueSql("GETUTCDATE()")
                );
        }
    }
    
}
