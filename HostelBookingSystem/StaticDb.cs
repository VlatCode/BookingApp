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
                    Id = 0,
                    Availability = true,
            },
            new Room {
                    Id = 1,
                    Availability = true
            },
            new Room {
                    Id = 2,
                    Availability = true
            }
        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation {
                    Id = 0,
                    MainGuest = new Guest
                    {
                        Id = 1,
                        Name = "Vlatko",
                    }
            },
            new Reservation {
                    Id = 1,
                    MainGuest = new Guest
                    {
                        Id = 1,
                        Name = "Viki",
                    }
            },
            new Reservation {
                    Id = 2,
                    MainGuest = new Guest
                    {
                        Id = 1,
                        Name = "Zoran",
                    }
            },
        };
    }
}
