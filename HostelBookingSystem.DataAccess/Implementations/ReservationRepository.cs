using HostelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DataAccess.Implementations
{
    public class ReservationRepository : IRepository<Reservation>
    {
        // In order to get access to the Reservation
        // we are dependent on the DataContext - it's a bridge to the database
        private DataContext _bookingAppDbContext;

        public ReservationRepository(DataContext bookingAppDbContext)
        {
            _bookingAppDbContext = bookingAppDbContext;
        }

        public void Add(Reservation entity)
        {
            _bookingAppDbContext.Reservations.Add(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public void Delete(Reservation entity)
        {
            _bookingAppDbContext.Reservations.Remove(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public List<Reservation> GetAll()
        {
            throw new NotImplementedException();
        }

        public Reservation GetById(int id)
        {
            return _bookingAppDbContext.Reservations
                .Include(x => x.Room)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Reservation entity)
        {
            _bookingAppDbContext.Reservations.Update(entity);
            _bookingAppDbContext.SaveChanges();
        }
    }
}
