using HostelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DataAccess.Implementations
{
    public class HostelRepository : IRepository<Hostel>
    {
        private DataContext _bookingAppDbContext;

        public HostelRepository(DataContext bookingAppDbContext)
        {
            _bookingAppDbContext = bookingAppDbContext;
        }

        public List<Hostel> GetAll()
        {
            return _bookingAppDbContext.Hostels
                .Include(x => x.Rooms)
                .ToList();
        }

        public Hostel GetById(int id)
        {
            return _bookingAppDbContext.Hostels
                .Include(x => x.Rooms)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Hostel entity)
        {
            _bookingAppDbContext.Hostels.Add(entity);
            _bookingAppDbContext.SaveChanges();
        }

        public void Update(Hostel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Hostel entity)
        {
            _bookingAppDbContext.Hostels.Remove(entity);
            _bookingAppDbContext.SaveChanges();
        }
    }
}
