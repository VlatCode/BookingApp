using HostelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DataAccess.Implementations
{
    public class RoomRepository : IRepository<Room>
    {
        private DataContext _bookingAppDbContext;

        public RoomRepository(DataContext bookingAppDbContext)
        {
            _bookingAppDbContext = bookingAppDbContext;
        }

        public void Add(Room entity)
        {
            _bookingAppDbContext.Rooms.Add(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public void Delete(Room entity)
        {
            _bookingAppDbContext.Rooms.Remove(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public List<Room> GetAll()
        {
            return _bookingAppDbContext.Rooms
                .Include(x => x.Hostel) // displays info about the Hostel
                .ToList();
        }

        public Room GetById(int id)
        {
            return _bookingAppDbContext.Rooms
                .Include(x => x.Hostel) // displays info about the Hostel
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Room entity)
        {
            _bookingAppDbContext.Rooms.Update(entity);
            _bookingAppDbContext.SaveChanges();
        }
    }
}
