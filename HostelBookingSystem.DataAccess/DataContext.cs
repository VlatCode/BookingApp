using HostelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ////////////////////////////
            // Hostel
            modelBuilder.Entity<Hostel>()
                .Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<Hostel>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<Hostel>()
                .Property(x => x.NumberOfRooms)
                .IsRequired();
            // Relation
            modelBuilder.Entity<Hostel>()
                .HasMany(x => x.Rooms)
                .WithOne(x => x.Hostel);

            ////////////////////////////
            // Room
            modelBuilder.Entity<Room>()
                .Property(x => x.Id)
                .IsRequired();
            // Relation
            modelBuilder.Entity<Room>()
                .HasOne(x => x.Hostel)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.HostelId);

            ////////////////////////////
            // Reservation
            modelBuilder.Entity<Reservation>()
                .Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<Reservation>()
                .Property(x => x.MainGuestId)
                .IsRequired();
            modelBuilder.Entity<Reservation>()
                .Property(x => x.StartDate)
                .IsRequired();
            modelBuilder.Entity<Reservation>()
                .Property(x => x.EndDate)
                .IsRequired();
            // Relation
            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.Room)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.RoomId);
            modelBuilder.Entity<Reservation>()
                .HasMany(x => x.Guests)
                .WithOne(x => x.Reservation);

            ////////////////////////////
            // Guest
            modelBuilder.Entity<Guest>()
                .Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<Guest>()
                .Property(x => x.Name)
                .IsRequired();
            // Relation
            modelBuilder.Entity<Guest>()
                .HasOne(x => x.Reservation)
                .WithMany(x => x.Guests)
                .HasForeignKey(x => x.ReservationId);
        }
    }
}
