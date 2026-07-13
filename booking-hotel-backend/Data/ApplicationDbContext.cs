namespace booking_hotel_backend.Data

{
    using booking_hotel_backend.Models.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasConversion<string>();
            modelBuilder.Entity<HotelAmenity>()
        .HasKey(x => new { x.HotelId, x.AmenityId });
            modelBuilder.Entity<Room>()
    .Property(x => x.Status)
    .HasConversion<string>();
        }

    }
}
