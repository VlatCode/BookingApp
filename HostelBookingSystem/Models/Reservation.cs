namespace HostelBookingSystem.Models
{
    public class Reservation
    {
        // The reservation should contain info about who made it
        // and number of quests along with their names.
        // This should be registered in the database.
        public int Id { get; set; }
        public Guest MainGuest { get; set; }
        public List<Guest> Guests = new List<Guest>();
        public int RoomId { get; set; }
    }
}
