namespace HostelBookingSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public bool Availability { get; set; }
        public int HostelId { get; set; }
    }
}
