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
            },

            new Hostel {
                    Id = 1,
                    Name = "4 Star Hostel",
            },
            new Hostel {
                    Id = 2,
                    Name = "5 Star Hostel",
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
                    StartDate = 1,
                    EndDate = 7
            },
            new Reservation {
                    Id = 1,
                    StartDate = 1,
                    EndDate = 5
            },
            new Reservation {
                    Id = 2,
                    StartDate = 6,
                    EndDate = 10
            },
        };
    }
}
