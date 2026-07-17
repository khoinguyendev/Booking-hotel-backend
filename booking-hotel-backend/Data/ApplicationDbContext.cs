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
        public DbSet<HotelBrand> HotelBrands { get; set; }
        public DbSet<Position> Positions { get; set; }

        public DbSet<HotelStaff> HotelStaffs { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelAmenity> HotelAmenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
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
