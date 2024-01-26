using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HostelBookingSystem.Domain.Models;

namespace HostelBookingSystem.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // The reservation should contain info about who made it
        // and number of quests along with their names.
        // This should be registered in the database.
        public int Id { get; set; }
        public int StartDate { get; set; } // CHANGE TO DATETIME and fix its implementations
        public int EndDate { get; set; } // CHANGE TO DATEIME and fix its implementations
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public User User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        // public int NumberOfGuests { get; set; } - implement property
    }
}
