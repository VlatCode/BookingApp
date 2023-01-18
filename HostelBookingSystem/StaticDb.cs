using HostelBookingSystem.Models;

namespace HostelBookingSystem
{
    public static class StaticDb
    {
        public static List<Hostel> Hostels = new List<Hostel>
        {
            new Hostel {
                    Id = 0,
                    Name = "3 Star Hostel",
                    NumberOfRooms = 3
            },

            new Hostel {
                    Id = 1,
                    Name = "4 Star Hostel",
                    NumberOfRooms = 5
            },
            new Hostel {
                    Id = 2,
                    Name = "5 Star Hostel",
                    NumberOfRooms = 10
            }
        };

        public static List<Room> Rooms = new List<Room>
        {
            new Room {
                    Id = 0
            },
            new Room {
                    Id = 1
            },
            new Room {
                    Id = 2
            }
        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation {
                    Id = 0,
                    MainGuestId = 0
            },
            new Reservation {
                    Id = 1,
                    MainGuestId = 1
            },
            new Reservation {
                    Id = 2,
                    MainGuestId = 2
            },
        };
    }
}
