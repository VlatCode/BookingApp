namespace HostelBookingSystem.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ReservationId { get; set; }
    }
}
