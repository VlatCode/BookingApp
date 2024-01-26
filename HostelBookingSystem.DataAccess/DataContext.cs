using HostelBookingSystem.Domain.Models;
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

        public DbSet<User> Users { get; set; }
        public DbSet<Hostel> Hostels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ////////////////////////////
            ///// USER
            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.FirstName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.LastName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.DateOfBirth)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.EmailAddress)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(x => x.PasswordSalt)
                .IsRequired();
            modelBuilder.Entity<User>()
                .HasMany(x => x.Reservations)
                .WithOne(x => x.User);

            ////////////////////////////
            // HOSTEL
            modelBuilder.Entity<Hostel>()
                .Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<Hostel>()
                .Property(x => x.Name)
                .IsRequired();
            modelBuilder.Entity<Hostel>()
                .HasMany(x => x.Rooms)
                .WithOne(x => x.Hostel);

            ////////////////////////////
            // ROOM
            modelBuilder.Entity<Room>()
                .Property(x => x.Id)
                .IsRequired();
            // Relation
            modelBuilder.Entity<Room>()
                .HasOne(x => x.Hostel)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.HostelId);

            ////////////////////////////
            // RESERVATION
            modelBuilder.Entity<Reservation>()
                .Property(x => x.Id)
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
                .HasOne(x => x.User)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.UserId);
        }
    }
}
