using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public List<Guest> Guests = new List<Guest>();
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }
}
