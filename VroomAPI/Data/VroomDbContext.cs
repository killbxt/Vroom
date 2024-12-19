using Microsoft.EntityFrameworkCore;
using VroomAPI.Models.Cars;
using VroomAPI.Models.Renters;
using VroomAPI.Models.Reservation;

namespace VroomAPI.Data
{
    public class VroomDbContext : DbContext
    {
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public VroomDbContext(DbContextOptions<VroomDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string decimalType = "decimal(18,2)";

            modelBuilder.Entity<Reservation>()
                 .Property(e => e.PaymentStatus)
                 .HasConversion<int>();

            modelBuilder.Entity<Reservation>()
                 .Property(e => e.RentalStatus)
                 .HasConversion<int>();

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Renter)
                .WithMany()
                .HasForeignKey(r => r.RenterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.StartDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Reservation>()
                .Property(r => r.RentalCost)
                .HasColumnType(decimalType);




            modelBuilder.Entity<Renter>()
                .Property(r => r.RegistrationDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Renter>()
               .Property(r => r.Rating)
               .HasColumnType(decimalType);





            modelBuilder.Entity<Car>()
                .Property(c => c.DailyRate)
                .HasColumnType(decimalType);

            modelBuilder.Entity<Car>()
               .Property(c => c.HourlyRate)
               .HasColumnType(decimalType);

        }
    }
}
