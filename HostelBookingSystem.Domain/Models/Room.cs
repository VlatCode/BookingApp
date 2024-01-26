using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelBookingSystem.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Hostel Hostel { get; set; }
        public int HostelId { get; set; }
        public List<Reservation> Reservations = new List<Reservation>();
        // public string Description { get; set; } - implement property
    }
}
