namespace HostelBookingSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public List<Reservation> Reservations = new List<Reservation>();

        public int HostelId { get; set; }
    }
}
